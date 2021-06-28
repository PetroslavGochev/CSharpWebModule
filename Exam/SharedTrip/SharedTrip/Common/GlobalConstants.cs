namespace SharedTrip.Common
{
    public class GlobalConstants
    {
        // VALIDATION 

        public const string RequiredField = "The field {0} is required";

        public const string MaxMinLength = "{0} is not valid. {0} should be between {1} and {2} length.";

        public const string EqualPassword = "Password and confirm password don`t match";

        public const string Exist = "This {0} already exist";

        public const string InvalidUserOrPassword = "Invalid username or password. Please try again!";

        public const string SeatsRange = "Seats must be between {0} and {1}.";

        public const string DescriptionLength = "Description must be only {0} characterss";

        public const string Username = "Username";

        public const string Password = "Password";

        public const string ConfirmPassword = "ConfirmPassword";

        public const string Email = "Email";

        public const string StartPoint = "Start Point";

        public const string EndPoint = "End Point";

        public const string DepartureTime = "Departure Time";

        public const string Seats = "Seats";

        public const string Description = "Description";


        // Users

        public const int MinLengthUsername = 5;

        public const int MaxLengthUsername = 20;

        public const int MinPasswordLength = 6;

        public const int MaxPasswordLength = 20;

        // Trips

        public const int FreeSeats = 0;

        public const int MinSeats = 5;

        public const int MaxSeats = 20; 

        public const int MaxDescriptionLength = 80;

        public const string DateTimeFormat = "dd.MM.yyyy HH:mm";
    }
}
