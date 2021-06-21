namespace CarShop.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
