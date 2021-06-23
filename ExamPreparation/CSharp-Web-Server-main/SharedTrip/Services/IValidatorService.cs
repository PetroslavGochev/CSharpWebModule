namespace SharedTrip.Services
{
    using SharedTrip.ViewModel.Trip;
    using SharedTrip.ViewModel.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidator(RegisterViewModel input);

        IEnumerable<string> TripsValidator(AddTripViewModel input);

        //IEnumerable<string> IssueValidator(string description);
    }
}
