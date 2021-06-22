namespace SulsProblemDescription.Services
{
    using SulsApp.Data;
    using SulsProblemDescription.Data.Model;
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Models.Submission;
    using System.Collections.Generic;
    using System.Linq;

    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext db;

        public ProblemService(
            ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<ProblemsViewModel> AllProblems()
        {
            var problems = this.db.Problems.ToArray();

            var collection = new List<ProblemsViewModel>();
            foreach (var problem in problems)
            {
                
                var problemViewModel = new ProblemsViewModel()
                {
                    Id = problem.Id,
                    Name = problem.Name,
                    Count = this.db.Submissions.Where(s => s.ProblemId == problem.Id).ToList().Count
                };

                collection.Add(problemViewModel);
            }

            return collection;
        }

        public void CreateProblem(CreateProblemViewModel model)
        {
            var problem = new Problem()
            {
                Name = model.Name,
                Points = model.Points,
            };

            this.db.Problems.Add(problem);

            this.db.SaveChanges();
        }

        public IEnumerable<SubmissionViewModel> DetailsProblem(string id)
        {
            var submissions = this.db.Submissions.Where(x => x.ProblemId == id).ToArray();

            var submissionsViewModel = new List<SubmissionViewModel>();
            foreach (var submission in submissions)
            {
                var submiss = new SubmissionViewModel()
                {
                    Id = submission.Id,
                    AchievedResult = submission.AchievedResult,
                    MaxPoints = this.db.Problems.FirstOrDefault(p => p.Id == submission.ProblemId).Points,
                    CreatedOn = submission.CreatedOn,
                    Name = this.db.Users.FirstOrDefault(u => u.Id == submission.UserId).Username,
                    Username = this.db.Problems.FirstOrDefault(s => s.Id == id).Name,
                };
                submissionsViewModel.Add(submiss);
            }

            return submissionsViewModel;
        }
    }
}
