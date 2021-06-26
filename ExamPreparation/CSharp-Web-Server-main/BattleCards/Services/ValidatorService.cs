namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using BattleCards.ViewModels.Users;
    using System.Collections.Generic;

    using static Common.GlobalConstants;

    public class ValidatorService : IValidatorService
    {
        private static string Name = "Name";
        private static string Description = "Description";
        private static string Attack = "Attack";
        private static string Health = "Health";
        private static string Keyword = "Keyword";
        private static string Image = "ImageUrl";
        private static string Username = "Username";
        private static string Password = "Password";
        private static string ConfirmPassword = "ConfirmPassword";
        private static string Email = "Email";

        private readonly IUserService userService;
        private List<string> errorMessage;

        public ValidatorService(IUserService userService)
        {
            this.userService = userService;
            this.errorMessage = new List<string>();
        }

        public IEnumerable<string> CardValidation(AddViewModel model)
        {
            this.Reuired(model.Name, Name);
            this.MinMaxLengthValidation(model.Name, Description, MinLengthName, MaxLengthName);

            this.Reuired(model.Description, Description);
            this.MinMaxLengthValidation(model.Description, Description, Minimum, DescriptionMaxLength );

            this.IsNegative(model.Attack, Attack);
            this.IsNegative(model.Health, Health);

            this.Reuired(model.Keyword, Keyword);

            this.Reuired(model.Image, Image);
    
            return this.errorMessage;
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

        private void IsNegative(int points, string field)
        {
            if (points < Minimum)
            {
                this.errorMessage.Add(string.Format(NotNegative, field));
            }
        }
    }
}
