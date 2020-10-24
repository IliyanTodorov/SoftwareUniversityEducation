using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            // validation

            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Add()
        {
            // validation

            return this.View();
        }       

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            // validation

            this.tripsService.CreateTrip(input);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            // validation

            var trip = this.tripsService.GetById(tripId);

            var model = new DetailsViewModel
            {
                ImagePath = trip.ImagePath,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime,
                Seats = trip.Seats,
                Description = trip.Description
            };

            return this.View(model);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            // validation 

            this.tripsService.AddUserToTrip(this.User, tripId);
            return this.Redirect("/");
        }
    }
}
