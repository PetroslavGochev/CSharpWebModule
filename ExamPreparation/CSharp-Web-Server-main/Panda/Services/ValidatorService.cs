namespace Panda.Services
{
    using Panda.ViewModels.Package;
    using Panda.ViewModels.User;
    using System.Collections.Generic;

    public class ValidatorService : IValidatorService
    {
        private const string REQUIRED = "The field {0} is required";
        private const string MaxMinLength = "{0} is not valid. {0} should be between 4 and 20 length.";
        private const string EqualPassword = "Password and confirm password don`t match";
        private const string Exist = "This {0} already exist";

        private readonly IUserService userService;
        private List<string> errorMessage;

        public ValidatorService(IUserService userService)
        {
            this.userService = userService;
            this.errorMessage = new List<string>();
        }

        public IEnumerable<string> PackageValidator(CreateViewModel input)
        {
            if (input.Description == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }
            else if (input.Description.ToString().Length < 5 || input.Description.ToString().Length > 20)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, "Username"));
            }

            return this.errorMessage;
        }

        public IEnumerable<string> UserValidator(RegisterViewModel model)
        {
            if (model.Username == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }
            else if (model.Username.ToString().Length < 4 || model.Username.ToString().Length > 20)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, "Username"));
            }

            if (model.Email == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "email"));
            }

            if (model.Password == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "password"));
            }
            else if (model.ConfirmPassword == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "confirmpassowrd"));
            }
            else if (model.ConfirmPassword != model.Password)
            {
                this.errorMessage.Add(EqualPassword);
            }

            if (this.userService.IsEmailExist(model.Email))
            {
                this.errorMessage.Add(string.Format(Exist, "email address"));
            }

            if (this.userService.IsUsernameExist(model.Username))
            {
                this.errorMessage.Add(string.Format(Exist, "username"));
            }

            return errorMessage;
        }
    }
}
