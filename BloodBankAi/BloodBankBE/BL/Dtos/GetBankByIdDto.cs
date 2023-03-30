
namespace BL.Dtos
{
    public class GetBankByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[]? Picture { get; set; }
        public ICollection<AddressBankDto>? Address { get; set; }

    }
    public class AddressBankDto
    {
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
    }

}
