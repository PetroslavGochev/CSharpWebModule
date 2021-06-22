namespace SulsProblemDescription.Services
{
    using SulsApp.Data;
    using SulsProblemDescription.Data.Model;
    using SulsProblemDescription.Models.Submission;
    using System;
    using System.Linq;

    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext db;

        public SubmissionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateSubmission(CreateSubmissionViewModel model)
        {
            var random = new Random();
            var maxPoints = this.db.Problems.FirstOrDefault(p => p.Id == model.ProblemId).Points;
            var achieved = random.Next(0, maxPoints);
            var submission = new Submission()
            {
                Code = model.Code,
                ProblemId = model.ProblemId,
                UserId = model.UserId,
                AchievedResult = achieved
            };

            this.db.Submissions.Add(submission);

            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var submission = this.db.Submissions.FirstOrDefault(s => s.Id == id);

            this.db.Submissions.Remove(submission);

            this.db.SaveChanges();
        }
    }
}
