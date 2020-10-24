using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext dbContext;

        public TripsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            if (this.dbContext.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId))
            {
                return;
            }

            this.dbContext.UserTrips.Add(new UserTrip
            {
                UserId = userId,
                TripId = tripId
            });
            this.dbContext.SaveChanges();
        }

        public void CreateTrip(AddTripInputModel input)
        {
            var trip = new Trip
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = DateTime.ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = input.ImagePath,
                Seats = input.Seats,
                Description = input.Description
            };

            this.dbContext.Trips.Add(trip);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = this.dbContext.Trips.Select(t => new TripViewModel
            {
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime,
                Seats = t.Seats,
                Id = t.Id
            }).ToArray();

            return trips;
        }

        public Trip GetById(string tripId)
        {
            return this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);
        }


    }
}
 