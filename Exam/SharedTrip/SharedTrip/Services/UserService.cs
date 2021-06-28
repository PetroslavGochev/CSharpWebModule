namespace SharedTrip.Services
{
    using System.Linq;

    using SharedTrip.Data;
    using SharedTrip.Data.Model;
    using SharedTrip.Services.Contracts;
    using SharedTrip.ViewModel.User;

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

        public string Login(LoginViewModel model)
        {
            var passwordHash = this.passwordService.HashPassword(model.Password);
            var users = this.db.USers.FirstOrDefault(u => u.Username == model.Username && u.Password == passwordHash);

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
