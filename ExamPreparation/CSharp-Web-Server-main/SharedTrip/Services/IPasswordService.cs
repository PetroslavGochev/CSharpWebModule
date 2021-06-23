namespace SharedTrip.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
