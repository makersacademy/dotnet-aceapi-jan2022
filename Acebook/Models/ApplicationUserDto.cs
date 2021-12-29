using System.Text.Json.Serialization;

namespace Acebook.Models
{
    public class ApplicationUserDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
