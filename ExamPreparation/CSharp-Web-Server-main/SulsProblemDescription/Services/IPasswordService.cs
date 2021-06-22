namespace SulsProblemDescription.Services
{
    public interface IPasswordService
    {
        string Hash(string password);
    }
}
