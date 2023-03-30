
namespace DAL.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int? BankId { get; set; }
        public Bank? Bank { get; set; }
    }
}
