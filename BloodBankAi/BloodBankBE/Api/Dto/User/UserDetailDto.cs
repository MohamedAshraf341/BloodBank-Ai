namespace Api.Dto.User
{
    public class UserDetailDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public bool Available { get; set; }
        public int? Age { get; set; }
        public byte[]? Picture { get; set; }
    }

}
