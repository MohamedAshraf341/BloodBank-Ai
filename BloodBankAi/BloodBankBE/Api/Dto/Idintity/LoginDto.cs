using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Idintity
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
