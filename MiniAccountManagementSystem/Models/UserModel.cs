namespace MiniAccountManagementSystem.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public int? SelectedRoleId { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}
