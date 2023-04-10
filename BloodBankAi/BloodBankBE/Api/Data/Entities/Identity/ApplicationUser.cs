using Microsoft.AspNetCore.Identity;

namespace Api.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public bool? Available { get; set; }
        public DateTime? LastActive { get; set; }
        public string? BloodGroup { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Moderator>? Moderates { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
