namespace SharedTrip.Controllers
{
    using SharedTrip.Services;
    using SharedTrip.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allTrips = this.tripsService.GetAll();
            return this.View(allTrips);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel input)
        {
            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.View();
            }

            this.tripsService.Add(input);

            return this.View("/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsService
                .GetById(tripId);

            if (trip == null)
            {
                return this.Error("Trip not found.");
            }

            return this.View(trip);
        }


        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.tripsService.GetById(tripId) == null)
            {
                return this.Error("Trip Not Found");
            }

            var userId = this.User;
            var isUserAlreadyInTrip = this.tripsService.IsUserAlreadyInCurrentTrip(userId, tripId);

            if (isUserAlreadyInTrip)
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.tripsService.AddUserToTrip(userId, tripId);

            return this.Redirect("/");
        }
    }
}
