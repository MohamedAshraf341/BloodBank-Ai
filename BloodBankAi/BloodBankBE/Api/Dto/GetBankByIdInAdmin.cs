namespace Api.Dto
{
    public class GetBankByIdInAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[]? Picture { get; set; }
        public AddressDto? Address { get; set; }
        public ICollection<ModeratorDto>? Moderators { get; set; }
    }
}
