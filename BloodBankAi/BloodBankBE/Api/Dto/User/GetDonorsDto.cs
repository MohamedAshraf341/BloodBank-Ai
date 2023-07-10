namespace Api.Dto.User
{
    public class GetDonorsDto
    {
        public string? Id { get; set; }
        public byte[]? Picture { get; set; }
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }
        public string? Name { get; set; }
    }
}
