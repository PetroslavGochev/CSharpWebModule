namespace Panda.Services
{
    using Panda.Data;
    using Panda.Data.Models;
    using Panda.ViewModels.User;
    using System.Collections.Generic;
    using System.Linq;

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

        public IEnumerable<string> AllUsers()
        {
            return this.db.Users.Select(u => u.Username).ToArray();
        }

        public string CurrentUsername(string id)
        {
            var user = this.db.Users.Find(id);

            return user.Username;
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
                Email = model.Email
            };

            this.db.Users.Add(user);

            this.db.SaveChanges();
        }

        public string UserId(string username)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Username == username);

            return user.Id;
        }
    }
}
