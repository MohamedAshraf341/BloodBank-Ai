namespace BL.Dtos
{
    public class AddUsersDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? BloodGroup { get; set; }
        public bool Available { get; set; }
        public AddressUser? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LastActive { get; set; }
        public PhotoUser? Photo { get; set; }
    }
    public class AddressUser
    {
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class PhotoUser
    {
        public string Url { get; set; }
    }
}
