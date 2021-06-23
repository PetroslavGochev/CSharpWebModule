namespace SharedTrip.Services
{
    using SharedTrip.ViewModel.Trip;
    using SharedTrip.ViewModel.User;
    using System.Collections.Generic;

    public class ValidatorService : IValidatorService
    {
        private const string REQUIRED = "The field {0} is required";
        private const string MaxMinLength = "{0} is not valid. {0} should be between 4 and 20 length.";
        private const string EqualPassword = "Password and confirm password don`t match";
        private const string Exist = "This {0} already exist";
        private const string InvalidPlateNumber = "This plate number is invalid";

        private readonly IUserService userService;
        private List<string> errorMessage;

        public ValidatorService(IUserService userService)
        {
            this.userService = userService;
            this.errorMessage = new List<string>();
        }

        public IEnumerable<string> UserValidator(RegisterViewModel input)
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

        public IEnumerable<string> TripsValidator(AddTripViewModel input)
        {
            if (input.StartPoint == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            if (input.EndPoint == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            if (input.Description == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            if (input.DepartureTime == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            if (input.Description.Length > 80)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "model"));
            }

            return this.errorMessage;
        }

        //public IEnumerable<string> IssueValidator(string description)
        //{
        //    if (description == null)
        //    {
        //        this.errorMessage.Add(string.Format(REQUIRED, "description"));
        //    }

        //    return this.errorMessage;
        //}
    }
}
