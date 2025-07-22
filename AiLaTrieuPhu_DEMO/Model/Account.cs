namespace AiLaTrieuPhu_DEMO.Model
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }     
        public string Role { get; set; }      // "Admin" hoặc "Guest"
        public bool IsLocked { get; set; }
    }
}
