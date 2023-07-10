namespace Api.Dto.Bank
{
    public class SeedBanksDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<BloodGroupBAnk> BloodGroups { get; set; }
        public AddressBank Address { get; set; }
        public string LastUpdated { get; set; }
        public List<ModeratorBank> Moderators { get; set; }
        public string Website { get; set; }
        public PhotoBank Photo { get; set; }
    }

    public class AddressBank
    {
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class BloodGroupBAnk
    {
        public string Group { get; set; }
        public int Value { get; set; }
    }

    public class ModeratorBank
    {
        public User User { get; set; }
    }

    public class PhotoBank
    {
        public string Url { get; set; }
    }


    public class User
    {
        public string UserName { get; set; }
    }

}
