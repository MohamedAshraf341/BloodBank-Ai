using Microsoft.AspNetCore.Http;
namespace BL.Dtos
{
    public class BankDto
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public IFormFile? Picture { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public string UserName { get; set; }
    }
}
