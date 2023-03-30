using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class Moderator
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int? BankId { get; set; }
        public Bank? Bank { get; set; }
        public string Type { get; set; }
    }
}
