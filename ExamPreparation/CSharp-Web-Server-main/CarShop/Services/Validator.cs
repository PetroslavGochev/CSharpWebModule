namespace CarShop.Services
{
    using CarShop.Models.Cars;
    using CarShop.Models.Users;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Validator : IValidator
    {       
        private const string REQUIRED = "The field {0} is required";
        private const string MaxMinLength = "{0} is not valid. {0} should be between 4 and 20 length.";
        private const string EqualPassword = "Password and confirm password don`t match";
        private const string Exist = "This {0} already exist";
        private const string InvalidPlateNumber = "This plate number is invalid";
        
        private readonly IUserService userService;
        private List<string> errorMessage;

        public Validator(IUserService userService)
        {
            this.userService = userService;
            this.errorMessage = new List<string>();
        }

        public IEnumerable<string> UserValidator(RegisterUserViewModel input)
        {
            if (input.Username == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }
            else if (input.Username.ToString().Length < 4 || input.Username.ToString().Length > 20)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, "Username"));
            }

            if (input.Email == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "email"));
            }

            if (input.Password == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "password"));
            }
            else if (input.ConfirmPassword == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "confirmpassowrd"));
            }
            else if (input.ConfirmPassword != input.Password)
            {
                this.errorMessage.Add(EqualPassword);
            }

            if (this.userService.IsEmailExist(input.Email))
            {
                this.errorMessage.Add(string.Format(Exist, "email address"));
            }

            if (this.userService.IsUsernameExist(input.Username))
            {
                this.errorMessage.Add(string.Format(Exist, "username"));
            }

            return errorMessage;
        }

        public IEnumerable<string> CarValidator(AddCarViewModel input)
        {
            if (input.Model == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }
            else if (input.Model.Length < 5 || input.Model.Length > 20)
            {
               this.errorMessage.Add(string.Format(MaxMinLength, "Model"));
            }

            if (input.Year == 0)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "year"));
            }

            if (input.PlateNumber == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "plate number"));
            }

            string plateNumber = @"[A-Z]{2}[0-9]{4}[A-Z]{2}$";

            Match matches = Regex.Match(input.PlateNumber, plateNumber);

            if (matches == null)
            {
                this.errorMessage.Add(plateNumber);
            }

            return this.errorMessage;
        }

        public IEnumerable<string> IssueValidator(string description)
        {
            if (description == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "description"));
            }

            return this.errorMessage;
        }
    }
}
