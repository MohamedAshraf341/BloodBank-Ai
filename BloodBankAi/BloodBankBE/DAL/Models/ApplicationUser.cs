using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime? LastActive { get; set; }
        public string? BloodGroup { get; set; }
        public bool? Available { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
        public Address? Address { get; set; }
        public ICollection<Moderator>? Moderates { get; set; }
    }
}