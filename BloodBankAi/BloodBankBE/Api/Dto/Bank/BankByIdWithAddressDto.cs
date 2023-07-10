using Api.Dto.Address;

namespace Api.Dto.Bank
{
    public class BankByIdWithAddressDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[]? Picture { get; set; }
        public AddressDto? Address { get; set; }

    }
}
