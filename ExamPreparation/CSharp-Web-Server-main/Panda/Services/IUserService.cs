namespace Panda.Services
{
    using Panda.ViewModels.User;
    using System.Collections.Generic;

    public interface IUserService
    {
        void Register(RegisterViewModel model);

        string Login(LoginViewModel model);

        string CurrentUsername(string id);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);

        IEnumerable<string> AllUsers();

        string UserId(string username);
    }
}
