namespace SharedTrip.Services.Contracts
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
