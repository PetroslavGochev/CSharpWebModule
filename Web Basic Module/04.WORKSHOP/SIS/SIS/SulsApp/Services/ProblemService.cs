using SulsApp.Data.Model;

namespace SulsApp.Services
{
    public class ProblemService : IProblemService
    {
        private readonly SulsAppDbContext db;

        public ProblemService(SulsAppDbContext db)
        {
            this.db = db;
        }
        public void CreateProblem(string name, int points)
        {
            var problem = new Problem()
            {
                Name = name,
                Points = points,
            };
            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }
    }
}
