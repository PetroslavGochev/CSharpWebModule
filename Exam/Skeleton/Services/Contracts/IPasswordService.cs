namespace Skeleton.Services.Contracts
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
