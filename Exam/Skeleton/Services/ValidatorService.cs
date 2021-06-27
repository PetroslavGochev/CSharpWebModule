namespace Skeleton.Services
{
    using Skeleton.Services.Contracts;
    using Skeleton.ViewModels.User;
    using System.Collections.Generic;

    using static Common.GlobalConstants;

    public class ValidatorService : IValidatorService
    {
        private static string Username = "Username";
        private static string Password = "Password";
        private static string ConfirmPassword = "ConfirmPassword";
        private static string Email = "Email";

        private readonly IUserService userService;
        private readonly List<string> errorMessage;

        public ValidatorService(IUserService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<string> UserValidation(RegisterViewModel model)
        {
            this.Reuired(model.Username, Username);
            this.MinMaxLengthValidation(model.Username, Username, MinLengthUsername, MaxLengthUsername);

            this.Reuired(model.Username, Username);

            this.Reuired(model.Password, Password);
            this.Reuired(model.ConfirmPassword, ConfirmPassword);

            if (model.ConfirmPassword != model.Password)
            {
                this.errorMessage.Add(EqualPassword);
            }

            if (this.userService.IsEmailExist(model.Email))
            {
                this.errorMessage.Add(string.Format(Exist, Email));
            }

            if (this.userService.IsUsernameExist(model.Username))
            {
                this.errorMessage.Add(string.Format(Exist, Username));
            }

            return errorMessage;
        }

        private void Reuired(string text, string field)
        {
            if (text == null)
            {
                this.errorMessage.Add(string.Format(RequiredField, field));
            }
        }

        private void MinMaxLengthValidation(string text, string field, int minLength, int maxLength)
        {
            if (text.Length < minLength || text.Length > maxLength)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, field));
            }
        }
    }
}
