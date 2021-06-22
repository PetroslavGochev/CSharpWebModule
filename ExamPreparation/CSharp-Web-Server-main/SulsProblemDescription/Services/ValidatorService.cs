namespace SulsProblemDescription.Services
{
    using SulsApp.Data;
    using SulsProblemDescription.Models.Problem;
    using SulsProblemDescription.Models.User;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ValidatorService : IValidatorService
    {
        private readonly ApplicationDbContext dB;
        private List<string> errors;

        public ValidatorService(ApplicationDbContext dB)
        {
            this.errors = new List<string>();
            this.dB = dB;
        }

        public IEnumerable<string> ProblemValidation(CreateProblemViewModel model)
        {
            if (model.Name == null)
            {
                this.errors.Add("Error");
            }
            else if (model.Name.Length < 5 || model.Name.Length > 20)
            {
                this.errors.Add("error");
            }

            if (model.Points == 0)
            {
                this.errors.Add("Error");
            }
            else if (model.Points < 50 || model.Points > 300)
            {
                this.errors.Add("error");
            }

            return this.errors;
        }

        public IEnumerable<string> SubmissionValidation(string code)
        {
            if (code == null)
            {
                this.errors.Add("error");
            }
            return this.errors;
        }

        public IEnumerable<string> UserValidation(UserRegisterViewModel model)
        {
            if (model.Username == null)
            {
                this.errors.Add("Error");
            }
            else if (model.Username.Length < 4 || model.Username.Length > 20)
            {
                this.errors.Add("error");
            }
            else if (this.dB.Users.Any(u => u.Username == model.Username))
            {
                this.errors.Add("errors");
            }

            if (model.Password.Length > 20)
            {
                this.errors.Add("errors");
            }
            else if (model.Password != model.ConfirmPassword)
            {
                this.errors.Add("Errors");
            }
            else if (model.Password == null || model.ConfirmPassword == null)
            {
                this.errors.Add("Errors");
            }

            if (model.Email == null)
            {
                this.errors.Add("error");
            }

            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Match match = Regex.Match(model.Email, pattern);

            if (!match.Success)
            {
                this.errors.Add("errors");
            }

            return this.errors;
        }
    }
}
