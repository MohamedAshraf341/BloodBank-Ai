namespace BL.Dtos
{
    public class AddCityDto
    {
        public string? id { get; set; }
        public string governorate_id { get; set; }
        public string? city_name_ar { get; set; }
        public string? city_name_en { get; set; }
    }
    public class ListCities
    {
        public List<AddCityDto> AddCityDto { get; set; }
    }
}
