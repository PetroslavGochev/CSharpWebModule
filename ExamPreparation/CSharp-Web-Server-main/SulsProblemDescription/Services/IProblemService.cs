namespace SulsProblemDescription.Services
{
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Models.Submission;
    using System.Collections.Generic;

    public interface IProblemService
    {
        IEnumerable<ProblemsViewModel> AllProblems();

        IEnumerable<SubmissionViewModel> DetailsProblem(string id);

        void CreateProblem(CreateProblemViewModel model);
    }
}
