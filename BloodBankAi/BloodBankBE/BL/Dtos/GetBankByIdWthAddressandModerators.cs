
namespace BL.Dtos
{
    public class GetBankByIdWthAddressandModerators
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[]? Picture { get; set; }
        public ICollection<jsonAddress>? Address { get; set; }
        public ICollection<jsonModerators>? Moderators { get; set; }
    }
    public class jsonAddress
    {
        public int Id { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public int? BankId { get; set; }
    }
    public class jsonModerators
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public jsonUser? User { get; set; }
        public int? BankId { get; set; }
        public string Type { get; set; }
    }
    public class jsonUser
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public byte[]? Picture { get; set; }
    }
}
