namespace CarShop.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Enums;
    using CarShop.Models.Users;

    public class UserService : IUserService
    {
        private readonly CarShopDbContext db;
        private readonly IPasswordService passwordService;

        public UserService(CarShopDbContext db, IPasswordService passwordService)
        {
            this.db = db;
            this.passwordService = passwordService;
        }

        public bool IsEmailExist(string email)
            => this.db.Users.Any(u => u.Email == email);

        public bool IsMechanic(string id)
        {
            var user = this.db.Users
                .FirstOrDefault(u => u.Id == id);

            return user.IsMechanic;
        }

        public bool IsUsernameExist(string username)
            => this.db.Users.Any(u => u.Username == username);

        public void Register(RegisterUserViewModel input)
        {
            var user = new User()
            {
                Username = input.Username,
                Password = this.passwordService.HashPassword(input.Password),
                Email = input.Email,
                IsMechanic = input.UserType == UserType.Mechanic.ToString() ? true : false
            };

            this.db.Add(user);

            this.db.SaveChanges();
        }

        public User UserLogin(LoginUserViewModel input)
        {
            var password = this.passwordService.HashPassword(input.Password);

            return
                this.db.Users
                .FirstOrDefault(u => u.Password == password && u.Username == input.Username);
        }
    }
}
