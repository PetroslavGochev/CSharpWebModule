namespace IRunes.Services
{
    using IRunes.ViewModel.User;

    public interface IUserService
    {
        void Register(RegisterViewModel model);

        string Login(LoginViewModel model);

        string CurrentUserName(string id);

        bool IsUserExist(string username);

        bool IsEmailExist(string email);
    }
}
