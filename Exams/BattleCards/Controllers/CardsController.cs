namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            // ?? WHY this doesn't work ??
            //var cardsViewModel = this.cardsService.GetAll();
            //return this.View(cardsViewModel);

            AllCardsViewModel allCards = new AllCardsViewModel()
            {
                AllCards = this.cardsService.GetAll().ToList(),
            };

            return this.View(allCards);
        }

        public HttpResponse Collection(string userId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            UserCardsCollectionViewModel cards = new UserCardsCollectionViewModel()
            {
                Cards = this.cardsService.GetUserCollection(this.User).ToList(),
            };

            return this.View(cards);
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
        public HttpResponse Add(AddCardInputModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cardId = this.cardsService.AddCard(input);
            var userId = this.User;
            this.cardsService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
