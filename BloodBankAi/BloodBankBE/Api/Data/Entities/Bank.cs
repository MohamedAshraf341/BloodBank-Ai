using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Data.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[]? Picture { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<BloodGroup>? BloodGroups { get; set; }
        public ICollection<Moderator>? Moderators { get; set; }



    }
}
