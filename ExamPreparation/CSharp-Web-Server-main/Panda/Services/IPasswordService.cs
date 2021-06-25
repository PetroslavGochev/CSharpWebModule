namespace Panda.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
