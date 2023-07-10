using Api.Dto.Address;

namespace Api.Dto.Bank
{
    public class GetAllBanksInAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
        public AddressDto? Address { get; set; }
    }
}
