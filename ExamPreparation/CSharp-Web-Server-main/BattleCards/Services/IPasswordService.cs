namespace BattleCards.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
    }
}
