namespace CarShop.Models.Users
{
    using CarShop.Models.Enums;

    public class RegisterUserViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserType { get; set; }
    }
}
