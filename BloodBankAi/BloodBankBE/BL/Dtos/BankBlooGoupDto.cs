namespace BL.Dtos
{
    public class BankBlooGoupDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<BloodGroupDto>? BloodGroups { get; set; }
    }
}
