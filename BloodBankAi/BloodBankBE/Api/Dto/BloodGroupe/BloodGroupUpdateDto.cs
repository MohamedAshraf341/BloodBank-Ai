namespace Api.Dto.BloodGroupe
{
    public class BloodGroupUpdateDto
    {
        public int BankId { get; set; }
        public ICollection<BloodGroupDto>? Groups { get; set; }
    }
}
