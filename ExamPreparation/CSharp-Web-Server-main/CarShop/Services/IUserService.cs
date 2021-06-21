namespace CarShop.Services
{
    using CarShop.Data.Models;
    using CarShop.Models.Users;
    using System.Threading.Tasks;

    public interface IUserService
    {
        void Register(RegisterUserViewModel input);

        bool IsEmailExist(string email);

        bool IsUsernameExist(string username);

        bool IsMechanic(string id);

        User UserLogin(LoginUserViewModel input);
    }
}
