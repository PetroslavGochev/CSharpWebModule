namespace SharedTrip.Services
{
    using SharedTrip.ViewModel.User;

    public interface IUserService
    {
        void RegisterUser(RegisterViewModel model);

        bool IsEmailExist(string email);

        bool IsUsernameExist(string username);

        string Login(string username, string password);
    }
}
