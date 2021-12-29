using System;
using System.Text.Json.Serialization;

namespace Acebook.Models
{
    public class TokenDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
    }
}
