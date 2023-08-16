using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ToDo.Host.Dtos;
using ToDo.Shared.Commands;
using ToDo.Shared.Dtos;

namespace ToDo.Host.Handlers
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, UserDto?>
    {
        private readonly HttpClient _http;
        private readonly HttpClient _apiHttp;
        private readonly IConfiguration _configuration;

        public AuthenticationCommandHandler(IHttpClientFactory clientFactory,
            IConfiguration configuration)
        {
            _http = clientFactory.CreateClient("GitHub");
            _apiHttp = clientFactory.CreateClient("GitHubApi");
            _configuration = configuration;
        }

        public async Task<UserDto?> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            if (request.Code is null)
            {
                return null;
            }
            var clientId = _configuration["Auth:ClientId"];
            var clientSecret = _configuration["Auth:ClientSecret"];
            var authRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"/login/oauth/access_token?client_id={clientId}&client_secret={clientSecret}&code={request.Code}", UriKind.Relative)
            };
            authRequest.Headers.Accept.Add(new("application/json"));
            var authResult = await _http.SendAsync(authRequest);
            var authResponse = await authResult.Content.ReadFromJsonAsync<GitHubAuthResponse>();

            if (!authResult.IsSuccessStatusCode)
            {
                return null;
            }

            var userInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/user", UriKind.Relative)
            };
            userInfoRequest.Headers.Add("authorization", $"Bearer {authResponse?.AccessToken}");
            userInfoRequest.Headers.Accept.Add(new("application/json"));

            _apiHttp.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            var userInfoResult = await _apiHttp.SendAsync(userInfoRequest);
            var userInfo = await userInfoResult.Content.ReadFromJsonAsync<UserInfoDto>();

            var accessToken = GenerateToken(userInfo);

            return new UserDto
            {
                AccessToken = accessToken,
                UserName = userInfo?.Login,
                FullName = userInfo?.Name,
                Email = userInfo?.Email
            };
        }

        private string GenerateToken(UserInfoDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Auth:Issuer"],
              _configuration["Auth:Issuer"],
              new List<Claim>
              {
                  new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                  new Claim(ClaimTypes.Email, userInfo.Email),
                  new Claim(ClaimTypes.Name, userInfo.Name),
              },
              expires: DateTime.Now.AddHours(8),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
