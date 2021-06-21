namespace CarShop.Services
{
    using CarShop.Models.Cars;
    using CarShop.Models.Users;
    using System.Collections.Generic;

    public interface IValidator
    {
        IEnumerable<string> UserValidator(RegisterUserViewModel input);

        IEnumerable<string> CarValidator(AddCarViewModel input);

        IEnumerable<string> IssueValidator(string description);
    }
}
