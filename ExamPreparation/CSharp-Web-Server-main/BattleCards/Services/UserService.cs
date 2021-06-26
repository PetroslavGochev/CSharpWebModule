namespace BattleCards.Services
{
    using System.Linq;

    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.ViewModels.Users;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly IPasswordService passwordService;

        public UserService(
            ApplicationDbContext db,
            IPasswordService passwordService)
        {
            this.db = db;
            this.passwordService = passwordService;
        }

        public bool IsEmailExist(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameExist(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        public string Login(LoginViewModel model)
        {
            var password = this.passwordService.HashPassword(model.Password);
            var user = this.db.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == password);

            return user != null ? user.Id : string.Empty;
        }

        public void Register(RegisterViewModel model)
        {
           var password = this.passwordService.HashPassword(model.Password);

            var user = new User()
            {
                Username = model.Username,
                Password = password,
                Email = model.Email,
            };

            this.db.Users.Add(user);

            this.db.SaveChanges();
        }
    }
}
