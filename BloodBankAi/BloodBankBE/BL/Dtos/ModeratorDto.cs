using System.ComponentModel.DataAnnotations;
namespace BL.Dtos
{
    public class ModeratorDto
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Type { get; set; }

    }
}
