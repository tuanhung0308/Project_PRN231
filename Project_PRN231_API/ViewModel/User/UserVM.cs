namespace Project_PRN231_API.ViewModel.User
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? RoleName { get; set; }
    }
}
