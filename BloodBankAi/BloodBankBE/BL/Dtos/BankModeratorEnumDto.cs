namespace BL.Dtos
{
    public class BankModeratorEnumDto
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
