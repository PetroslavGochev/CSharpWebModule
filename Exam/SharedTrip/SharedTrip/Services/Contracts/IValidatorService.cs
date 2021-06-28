namespace SharedTrip.Services.Contracts
{
    using SharedTrip.ViewModel.Trip;
    using SharedTrip.ViewModel.User;
    using System.Collections.Generic;

    public interface IValidatorService
    {
        IEnumerable<string> UserValidation(RegisterViewModel model);

        IEnumerable<string> TripsValidation(AddTripViewModel model);
    }
}
