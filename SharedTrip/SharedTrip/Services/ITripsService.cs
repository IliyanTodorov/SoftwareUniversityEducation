namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.ViewModels.Trips;
    using System.Collections.Generic;

    public interface ITripsService
    {
        IEnumerable<TripViewModel> GetAll();

        void CreateTrip(AddTripInputModel input);
        Trip GetById(string tripId);
        void AddUserToTrip(string user, string tripId);
    }
}
