namespace Api.Dto
{
    public class AddBankModeratorsInAdminDto
    {
        public int BankId { get; set; }
        public string UserName { get; set; }
        public AdminstrationBank Roles { get; set; }
    }
    public enum AdminstrationBank
    {
        BankAdmin,
        BankModerator
    }
    public class EnumItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
