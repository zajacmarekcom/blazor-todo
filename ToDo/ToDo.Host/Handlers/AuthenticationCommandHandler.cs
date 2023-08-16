using MediatR;
using System.Net.Http.Headers;
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
            var authResult = await(await _http.SendAsync(authRequest)).Content.ReadFromJsonAsync<GitHubAuthResponse>();
            var userInfoRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("/user", UriKind.Relative)
            };
            userInfoRequest.Headers.Add("authorization", $"Bearer {authResult?.AccessToken}");
            userInfoRequest.Headers.Accept.Add(new("application/json"));
            //userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue(, );

            _apiHttp.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            var userInfoResult = await _apiHttp.SendAsync(userInfoRequest);
            var userInfo = await userInfoResult.Content.ReadFromJsonAsync<UserInfoDto>();

            return new UserDto
            {
                AccessToken = authResult?.AccessToken,
                RefreshToken = authResult?.RefreshToken,
                UserName = userInfo?.Login,
                FullName = userInfo?.Name,
                Email = userInfo?.Email
            };
        }
    }
}
