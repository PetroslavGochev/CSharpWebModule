namespace Skeleton.Common
{
    public class GlobalConstants
    { 
        // VALIDATION 

        public const string RequiredField = "The field {0} is required";

        public const string MaxMinLength = "{0} is not valid. {0} should be between {1} and {2} length.";

        public const string EqualPassword = "Password and confirm password don`t match";

        public const string Exist = "This {0} already exist";

        public const string InvalidUserOrPassword = "Invalid username or password. Please try again!";


        // Users

        public const int MinLengthUsername = 5;

        public const int MaxLengthUsername = 20;
    }
}
