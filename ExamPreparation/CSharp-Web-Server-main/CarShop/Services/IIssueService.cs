namespace CarShop.Services
{
    public interface IIssueService
    {
        void Add(string carId, string description);

        void Delete(string issueId);

        void Fix(string issueId);
    }
}
