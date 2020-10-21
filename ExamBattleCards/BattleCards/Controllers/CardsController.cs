namespace BattleCards.Controllers
{
    using BattleCards.Services;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardAddInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(inputModel.Name) || inputModel.Name.Length < 5 || inputModel.Name.Length > 15)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.Description) || inputModel.Description.Length > 200 || inputModel.Description.Length < 10)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.ImageURL))
            {
                return this.View();
            }

            if (inputModel.Attack < 0)
            {
                return this.Error("Attack should be non-negative integer.");
            }

            if (inputModel.Health < 0)
            {
                return this.Error("Health should be non-negative integer.");
            }

            if (string.IsNullOrEmpty(inputModel.Keyword))
            {
                return this.View();
            }

            var name = inputModel.Name;
            var imageURL = inputModel.ImageURL;
            var keyword = inputModel.Keyword;
            var attack = inputModel.Attack;
            var health = inputModel.Health;
            var description = inputModel.Description;

            var cardId = this.cardsService.AddCard(name, imageURL, keyword, attack, health, description);

            string userId = this.User;
            this.cardsService.AddCardToUserCollection(userId, cardId);
            
            return this.Redirect("/Cards/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cardsViewModel = this.cardsService.GetAll();
            return this.View(cardsViewModel);
        }
    }
}
