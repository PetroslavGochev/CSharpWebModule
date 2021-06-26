namespace BattleCards.Services
{
    using BattleCards.ViewModels.Users;

    public interface IUserService
    {
        void Register(RegisterViewModel model);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);

        string Login(LoginViewModel model);
    }
}
