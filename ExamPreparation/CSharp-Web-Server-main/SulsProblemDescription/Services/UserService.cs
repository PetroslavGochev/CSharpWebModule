namespace SulsProblemDescription.Services
{
    using SulsApp.Data;
    using SulsProblemDescription.Data.Model;
    using SulsProblemDescription.Models.User;
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

        public string Login(string username, string password)
        {
            var passwordHash = this.passwordService.Hash(password);
            var user = this.db.Users
                .FirstOrDefault(u => u.Username == username && u.Password == passwordHash);

            return user == null ? string.Empty : user.Id;
        }

        public void Register(UserRegisterViewModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = this.passwordService.Hash(model.Password),
                Email = model.Email,
            };

            this.db.Users.Add(user);

            this.db.SaveChanges();
        }
    }
}
