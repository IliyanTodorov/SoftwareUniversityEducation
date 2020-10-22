namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System;
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

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length < 5 || input.Name.Length > 15)
            {
                return this.Error("Name should be between 5 and 15 characters long.");
            }

            if (string.IsNullOrWhiteSpace(input.Image))
            {
                return this.Error("The image is required!");
            }

            if (!Uri.TryCreate(input.Image, UriKind.Absolute, out _))
            {
                return this.Error("Image url should be valid.");
            }

            if (string.IsNullOrWhiteSpace(input.Keyword))
            {
                return this.Error("Keyword is required.");
            }

            if (input.Attack < 0)
            {
                return this.Error("Attack should be non-negative integer.");
            }

            if (input.Health < 0)
            {
                return this.Error("Health should be non-negative integer.");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 200)
            {
                return this.Error("Description is required and its length should be at most 200 characters.");
            }

            var cardId = this.cardsService.AddCard(input);
            var userId = this.User;
            this.cardsService.AddCardToUserCollection(userId, cardId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.cardsService.AddCardToUserCollection(this.User, cardId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.cardsService.RemoveCardFromUserCollection(this.User, cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
