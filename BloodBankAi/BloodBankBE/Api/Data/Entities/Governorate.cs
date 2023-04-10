using System.ComponentModel.DataAnnotations;

namespace Api.Data.Entities
{
    public class Governorate
    {
        public int Id { get; set; }
        public string? ArabicName { get; set; }
        public string? EnglishName { get; set; }
        public ICollection<City>? Cities { get; set; }
    }
}
