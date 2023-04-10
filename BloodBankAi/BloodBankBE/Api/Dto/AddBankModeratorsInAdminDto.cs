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
}
