namespace MiniAccountManagementSystem.Models
{
    public class UserWithRolesModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
