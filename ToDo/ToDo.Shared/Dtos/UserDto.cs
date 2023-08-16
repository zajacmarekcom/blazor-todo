namespace ToDo.Shared.Dtos;

public class UserDto
{
    public string UserName { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
