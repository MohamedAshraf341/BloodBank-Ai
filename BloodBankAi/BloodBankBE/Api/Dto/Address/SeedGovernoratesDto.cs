namespace Api.Dto.Address
{
    public class SeedGovernoratesDto
    {
        public string? id { get; set; }
        public string? governorate_name_ar { get; set; }
        public string? governorate_name_en { get; set; }
    }
    public class ListGovernorates
    {
        public List<SeedGovernoratesDto> AddGovernoratesDto { get; set; }
    }
}
