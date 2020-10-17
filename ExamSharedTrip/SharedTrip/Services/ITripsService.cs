namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.ViewModels;
    using System.Collections.Generic;

    public interface ITripsService
    {
        void Add(TripInputModel input);

        IEnumerable<Trip> GetAll();

        Trip GetById(string id);

        void AddUserToTrip(string userId, string tripId);
        bool IsUserAlreadyInCurrentTrip(string userId, string tripId);
    }
}
