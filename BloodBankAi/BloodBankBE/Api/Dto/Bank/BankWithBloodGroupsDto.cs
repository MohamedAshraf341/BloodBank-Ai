using Api.Dto.BloodGroupe;

namespace Api.Dto.Bank
{
    public class BankWithBloodGroupsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<BloodGroupDto>? BloodGroups { get; set; }
    }
}
