using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using ToDo.Shared.Dtos;

namespace ToDo.Client.Shared.Services
{
    public class AccessTokenMessageHandler : DelegatingHandler
    {
        private readonly SessionStorageService _sessionStorageService;
        private readonly ToDoAuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigation;

        public AccessTokenMessageHandler(SessionStorageService sessionStorageService, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigation)
        {
            _sessionStorageService = sessionStorageService;
            _authenticationStateProvider = authenticationStateProvider as ToDoAuthenticationStateProvider;
            _navigation = navigation;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetAccessToken();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await _authenticationStateProvider.LogOutAsync();
                    _navigation.NavigateToLogin("/login");
                }
            }

            return response;
        }

        private async Task<string?> GetAccessToken()
        {
            var value = await _sessionStorageService.GetValueAsync("user");
            if (value == null)
            {
                return null;
            }

            var user = JsonSerializer.Deserialize<UserDto>(value);

            return user?.AccessToken;
        }
    }
}
