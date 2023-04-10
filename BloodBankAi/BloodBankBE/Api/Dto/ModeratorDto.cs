namespace Api.Dto
{
    public class ModeratorDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public UserModerator? User { get; set; }
        public int? BankId { get; set; }
        public string Type { get; set; }
    }
    public class UserModerator
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public byte[]? Picture { get; set; }
    }
}
