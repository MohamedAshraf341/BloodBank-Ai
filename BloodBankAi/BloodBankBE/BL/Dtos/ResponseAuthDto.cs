using System.Text.Json.Serialization;

namespace BL.Dtos
{
    public class ResponseAuthDto
    {
        public string? Message { get; set; }
        public string? Id { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiresOn { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}