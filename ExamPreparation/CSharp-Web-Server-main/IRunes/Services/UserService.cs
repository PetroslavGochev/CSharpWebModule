namespace IRunes.Services
{
    using System.Linq;

    using IRunes.Data;
    using IRunes.Data.Models;
    using IRunes.ViewModel.User;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly IPasswordService passwordService;

        public UserService(ApplicationDbContext db, IPasswordService passwordService)
        {
            this.db = db;
            this.passwordService = passwordService;
        }

        public string CurrentUserName(string id)
        {
            var user = this.db.Users.Find(id);

            return user.Username;
        }

        public bool IsEmailExist(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }

        public bool IsUserExist(string username)
        {
            return this.db.Users.Any(u => u.Username == username);
        }

        public string Login(LoginViewModel model)
        {
            var password = this.passwordService.HashPassword(model.Password);
            var user = this.db.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == password);

            return user != null ? user.Id : string.Empty;
        }

        public void Register(RegisterViewModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = this.passwordService.HashPassword(model.Password),
                Email = model.Email,
            };

            this.db.Users.Add(user);

            this.db.SaveChanges();
        }
    }
}
