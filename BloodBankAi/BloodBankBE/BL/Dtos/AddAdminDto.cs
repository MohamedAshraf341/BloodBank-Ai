

namespace BL.Dtos
{
    public class AddAdminDto
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
