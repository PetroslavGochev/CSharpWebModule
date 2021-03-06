using IRunes.ViewModel.Album;
using IRunes.ViewModel.Track;
using IRunes.ViewModel.User;
using System.Collections.Generic;

namespace IRunes.Services
{
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

        public IEnumerable<string> AlbumValidator(CreateAlbumViewModel input)
        {
            if (input.Name == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }
            else if (input.Name.ToString().Length < 4 || input.Name.ToString().Length > 20)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, "Username"));
            }

            if (input.Cover == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }

            return this.errorMessage;
        }

        public IEnumerable<string> TrackValidator(CreateTrackViewModel input)
        {
            if (input.Name == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }
            else if (input.Name.ToString().Length < 4 || input.Name.ToString().Length > 20)
            {
                this.errorMessage.Add(string.Format(MaxMinLength, "Username"));
            }

            if (input.Link == null)
            {
                this.errorMessage.Add(string.Format(REQUIRED, "username"));
            }

            return this.errorMessage;
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

            if (this.userService.IsUserExist(input.Username))
            {
                this.errorMessage.Add(string.Format(Exist, "username"));
            }

            return errorMessage;
        }
    }
}
