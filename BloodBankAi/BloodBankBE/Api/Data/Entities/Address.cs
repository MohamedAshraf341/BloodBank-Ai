
using Api.Data.Entities.Identity;

namespace Api.Data.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string? Area { get; set; }
        public string? City { get; set; }
        public string? Government { get; set; }
        public string? Country { get; set; }
        public ICollection<ApplicationUser>? User { get; set; }
        public ICollection<Bank>? Bank { get; set; }
    }
}
