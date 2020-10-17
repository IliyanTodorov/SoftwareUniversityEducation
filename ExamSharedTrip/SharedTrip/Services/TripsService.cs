namespace SharedTrip.Services
{
    using Microsoft.EntityFrameworkCore.Internal;
    using SharedTrip.Models;
    using SharedTrip.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext dbContext;

        public TripsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(TripInputModel input)
        {
            var parsedTime = DateTime
                .ParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

            string startingPoint = input.StartPoint;
            string endPoint = input.EndPoint;
            string imagePath = input.ImagePath;
            DateTime departureTime = parsedTime;
            int seats = input.Seats;
            string description = input.Description;

            var trip = new Trip()
            {
                StartPoint = startingPoint,
                EndPoint = endPoint,
                DepartureTime = departureTime,
                ImagePath = imagePath,
                Seats = seats,
                Description = description
            };

            this.dbContext.Trips.Add(trip);
            this.dbContext.SaveChanges();
        }

        public void AddUserToTrip(string userId, string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(t => t.Id == tripId);

            trip.Seats -= 1;

            var userTrip = new UserTrip()
            {
                UserId = userId,
                Trip = trip
            };

            this.dbContext.UserTrips.Add(userTrip);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Trip> GetAll()
        {
            return this.dbContext.Trips
                .Select(x => new Trip
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime,
                    ImagePath = x.ImagePath,
                    Seats = x.Seats,
                    Description = x.Description
                })
                .ToArray();
        }

        public Trip GetById(string id)
        {
            return this.dbContext.Trips.FirstOrDefault(x => x.Id == id);
        }

        public bool IsUserAlreadyInCurrentTrip(string userId, string tripId)
        {
            var trip = this.dbContext.Trips.FirstOrDefault(x => x.Id == tripId);

            if (trip == null)
            {
                return false;
            }

            return trip.Users.Any(u => u.UserId == userId);
        }
    }
}
