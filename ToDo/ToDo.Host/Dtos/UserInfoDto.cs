using System.Text.Json.Serialization;

namespace ToDo.Host.Dtos
{
    public class UserInfoDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
