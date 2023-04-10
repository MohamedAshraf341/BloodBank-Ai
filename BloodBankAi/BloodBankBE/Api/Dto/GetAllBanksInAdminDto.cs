namespace Api.Dto
{
    public class GetAllBanksInAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
        public AddressDto? Address { get; set; }
    }
}
