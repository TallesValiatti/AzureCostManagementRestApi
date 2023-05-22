using System.Text.Json.Serialization;

namespace AzureCostManagement.Api.Models
{
    public record AccessToken
    {
         [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = default!;

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("access_token")]
        public string Token { get; set; } = default!;
    }
}