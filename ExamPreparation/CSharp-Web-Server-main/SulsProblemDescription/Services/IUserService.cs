namespace SulsProblemDescription.Services
{
    using SulsProblemDescription.Data.Model;
    using SulsProblemDescription.Models.User;

    public interface IUserService
    {
        void Register(UserRegisterViewModel model);

        string Login(string username, string password);

    }
}
