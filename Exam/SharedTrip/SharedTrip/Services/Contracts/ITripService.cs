namespace SharedTrip.Services.Contracts
{
    using SharedTrip.ViewModel.Trip;
    using System.Collections.Generic;

    public interface ITripService
    {
        void CreateTrip(AddTripViewModel model);

        IEnumerable<AllTripViewModel> AllTrip();

        DetailsViewModel Details(string tripId);

        void AddUserToTrip(string tripId, string userId);

        bool IsTripUserExist(string userId, string tripId);
    }
}
