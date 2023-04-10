namespace Api.Dto
{
    public class GetDonorById
    {
        public byte[]? Picture { get; set; }
        public string? Name { get; set; }
        public string? BloodGroup { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public DateTime? LastActive { get; set; }

    }

}
