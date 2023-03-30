

namespace DAL.Models
{
    public class City
    {
        public int Id { get; set; }
        public int GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        public string? ArabicName { get; set; }
        public string? EnglishName { get; set; }
    }
}
