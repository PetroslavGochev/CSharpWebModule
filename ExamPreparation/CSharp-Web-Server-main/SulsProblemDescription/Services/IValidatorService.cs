namespace SulsProblemDescription.Services
{
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Models.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidation(UserRegisterViewModel model);

        IEnumerable<string> ProblemValidation(CreateProblemViewModel model);

        IEnumerable<string> SubmissionValidation(string code);
    }
}
