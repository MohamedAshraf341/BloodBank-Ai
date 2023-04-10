namespace Api.Dto
{
    public class SeedCitiesDto
    {
        public string? id { get; set; }
        public string governorate_id { get; set; }
        public string? city_name_ar { get; set; }
        public string? city_name_en { get; set; }
    }
    public class ListCities
    {
        public List<SeedCitiesDto> AddCityDto { get; set; }
    }
}
