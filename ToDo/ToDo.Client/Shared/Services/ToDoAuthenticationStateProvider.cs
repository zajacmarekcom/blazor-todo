using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ToDo.Shared.Dtos;

namespace ToDo.Client.Shared.Services;

public class ToDoAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly SessionStorageService _sessionStorageService;

    public ToDoAuthenticationStateProvider(SessionStorageService sessionStorageService)
    {
        _sessionStorageService = sessionStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await _sessionStorageService.GetValueAsync<UserDto>("user");

        ClaimsIdentity claims = new ClaimsIdentity();

        if (user is not null)
        {
            claims = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.UserName)
            }, "Api");
        }

        var principal = new ClaimsPrincipal(claims);

        return new AuthenticationState(principal);
    }

    public async Task AuthenticateAsync(UserDto? user)
    {
        await _sessionStorageService.SetValueAsync("user", user);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogOutAsync()
    {
        await _sessionStorageService.RemoveKeyAsync("user");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
