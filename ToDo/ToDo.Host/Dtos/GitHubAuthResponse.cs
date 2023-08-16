using System.Text.Json.Serialization;

namespace ToDo.Host.Dtos
{
    public class GitHubAuthResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
