namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using System.Linq;

    public class IssueService : IIssueService
    {
        private readonly CarShopDbContext db;

        public IssueService(CarShopDbContext db)
        {
            this.db = db;
        }

        public void Add(string carId, string description)
        {
            var issue = new Issue()
            {
                Description = description,
                IsFixed = false,
                CarId = carId,
            };

            this.db.Issues.Add(issue);

            this.db.SaveChanges();
        }

        public void Delete(string issueId)
        {
            var issue = this.db.Issues.FirstOrDefault(i => i.Id == issueId);

            this.db.Issues.Remove(issue);

            this.db.SaveChanges();
        }

        public void Fix(string issueId)
        {
            var issue = this.db.Issues.FirstOrDefault(i => i.Id == issueId);

            issue.IsFixed = true;

            this.db.SaveChanges();
        }
    }
}
