using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Idintity
{
    public class RegisterDto
    {
        


        [Required, StringLength(50)] public string Name { get; set; }
        [Required, StringLength(128)] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string BloodGroup { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public string Area { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Government { get; set; }
        [Required, StringLength(256)] public string Password { get; set; }

    }

}
