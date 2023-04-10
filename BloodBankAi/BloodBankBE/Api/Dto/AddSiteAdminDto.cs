namespace Api.Dto
{
    public class AddSiteAdminDto
    {
        public string? UserName { get; set; }
        public Adminstration Roles { get; set; }
    }
    public enum Adminstration
    {
        Admin,
        Moderator,
    }
}
