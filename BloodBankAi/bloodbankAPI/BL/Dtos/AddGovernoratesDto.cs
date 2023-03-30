namespace BL.Dtos
{
    public class AddGovernoratesDto
    {
        public string? id { get; set; }
        public string? governorate_name_ar { get; set; }
        public string? governorate_name_en { get; set; }
    }
    public class ListGovernorates
    {
        public List<AddGovernoratesDto> AddGovernoratesDto { get; set; }
    }
}
