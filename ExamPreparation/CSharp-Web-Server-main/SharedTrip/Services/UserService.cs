namespace SharedTrip.Services
{
    using SharedTrip.Data;
    using SharedTrip.Data.Model;
    using SharedTrip.ViewModel.User;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        private readonly IPasswordService passwordService;

        public UserService(ApplicationDbContext db, IPasswordService passwordService)
        {
            this.db = db;
            this.passwordService = passwordService;
        }

        public bool IsEmailExist(string email)
         => this.db.USers.Any(u => u.Email == email);

        public bool IsUsernameExist(string username)
         => this.db.USers.Any(u => u.Username == username);

        public string Login(string username, string password)
        {
            var passwordHash = this.passwordService.HashPassword(password);
            var users = this.db.USers.FirstOrDefault(u => u.Username == username && u.Password == passwordHash);

            return users == null ? string.Empty : users.Id;
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = this.passwordService.HashPassword(model.Password),
                Email = model.Email
            };

            this.db.USers.Add(user);

            this.db.SaveChanges();
        }
    }
}
