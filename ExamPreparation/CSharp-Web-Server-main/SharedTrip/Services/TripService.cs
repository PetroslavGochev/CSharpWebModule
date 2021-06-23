namespace SharedTrip.Services
{
    using SharedTrip.Data;
    using SharedTrip.Data.Model;
    using SharedTrip.ViewModel.Trip;
    using System.Collections.Generic;
    using System.Linq;

    public class TripService : ITripService
    {
        private readonly ApplicationDbContext db;

        public TripService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var trips = this.db.Trips.FirstOrDefault(t => t.Id == tripId);

            if (trips.Seats > 0)
            {
                var userTrips = new UserTrip()
                {
                    UserId = userId,
                    TripId = tripId,
                };

                this.db.Usertrips.Add(userTrips);
                trips.Seats--;
                this.db.SaveChanges(); 
            }
          
           
        }

        public IEnumerable<AllTripViewModel> AllTrip()
        {
            var trips = this.db.Trips.ToArray();

            var collection = new List<AllTripViewModel>();

            foreach (var trip in trips)
            {
                var tripModel = new AllTripViewModel()
                {
                    StartPoint = trip.StartPoint,
                    EndPoint = trip.EndPoint,
                    Seats = trip.Seats,
                    DepartureTime = trip.DepartureTime,
                    Id = trip.Id,
                };
                collection.Add(tripModel);
            }

            return collection;
        }

        public void CreateTrip(AddTripViewModel model)
        {
            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = model.DepartureTime,
                Description = model.Description,
                Seats = model.Seats,
                ImagePath = model.ImagePath
            };

            this.db.Trips.Add(trip);

            this.db.SaveChanges();
        }

        public DetailsViewModel Details(string tripId)
        {
            var trips = this.db.Trips.FirstOrDefault(t => t.Id == tripId);

            var tripsViewModel = new DetailsViewModel()
            {
                Id = trips.Id,
                StartPoint = trips.StartPoint,
                EndPoint = trips.EndPoint,
                DepartureTime = trips.DepartureTime,
                Seats = trips.Seats,
                Description = trips.Description,
                ImagePath = trips.ImagePath,
            };

            return tripsViewModel;
        }

        public bool IsTripUserExist(string userId, string tripId)
        {
            return this.db.Usertrips.Any(ut => ut.TripId == tripId && userId == ut.UserId);
        }
    }
}
