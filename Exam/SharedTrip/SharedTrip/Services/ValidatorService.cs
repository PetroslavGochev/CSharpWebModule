namespace SharedTrip.Services
{
    using SharedTrip.Services.Contracts;
    using SharedTrip.ViewModel.Trip;
    using SharedTrip.ViewModel.User;
    using System.Collections.Generic;

    using static Common.GlobalConstants;

    public class ValidatorService : IValidatorService
    {
        private readonly IUserService userService;
        private readonly List<string> errorMessage;

        public ValidatorService(IUserService userService)
        {
            this.userService = userService;
            this.errorMessage = new List<string>();
        }

        public IEnumerable<string> UserValidation(RegisterViewModel model)
        {
            this.Reuired(model.Username, Username);
            this.MinMaxLengthValidation(model.Username, Username, MinLengthUsername, MaxLengthUsername);

            this.Reuired(model.Password, Password);
            this.Reuired(model.ConfirmPassword, ConfirmPassword);

            if (model.ConfirmPassword != model.Password)
            {
                this.errorMessage.Add(EqualPassword);
            }

            this.MinMaxLengthValidation(model.Password, Password, MinPasswordLength, MaxPasswordLength);

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

        public IEnumerable<string> TripsValidation(AddTripViewModel model)
        {
            this.Reuired(model.StartPoint, StartPoint);
            this.Reuired(model.EndPoint, EndPoint);
            this.Reuired(model.Description, Description);
            this.Reuired(model.DepartureTime.ToString(), DepartureTime);


            if (model.Seats < MinSeats || model.Seats > MaxSeats)
            {
                this.errorMessage.Add(string.Format(Seats, MinSeats, MaxSeats));
            }

            if (model.Description.Length > MaxDescriptionLength)
            {
                this.errorMessage.Add(string.Format(DescriptionLength, MaxDescriptionLength));
            }

            return this.errorMessage;
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
                this.errorMessage.Add(string.Format(MaxMinLength, field, minLength, maxLength));
            }
        }
    }
}
