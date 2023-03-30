

using Microsoft.AspNetCore.Http;

namespace BL.Dtos
{
    public class UserProfileUpdateDto
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Government { get; set; }
        public string Country { get; set; }
        public bool Available { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
