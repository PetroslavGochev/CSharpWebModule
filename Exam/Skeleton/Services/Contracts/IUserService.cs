namespace Skeleton.Services.Contracts
{
    using Skeleton.ViewModels.User;

    public interface IUserService
    {
        void Register(RegisterViewModel model);

        string Login(LoginViewModel model);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);
    }
}
