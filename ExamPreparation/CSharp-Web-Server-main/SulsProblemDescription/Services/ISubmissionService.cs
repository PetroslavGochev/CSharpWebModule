namespace SulsProblemDescription.Services
{
    using SulsProblemDescription.Models.Submission;

    public interface ISubmissionService
    {
        void CreateSubmission(CreateSubmissionViewModel model);

        void Delete(string id);
    }
}
