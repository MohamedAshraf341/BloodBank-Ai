using Api.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Entities
{
    public class Moderator
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int? BankId { get; set; }
        public Bank? Bank { get; set; }
        public string? Type { get; set; }
    }
}
