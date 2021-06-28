namespace SharedTrip.Services.Contracts
{
    using SharedTrip.ViewModel.User;

    public interface IUserService
    {
        string Login(LoginViewModel model);

        void RegisterUser(RegisterViewModel model);

        bool IsEmailExist(string email);

        bool IsUsernameExist(string username);
    }
}
