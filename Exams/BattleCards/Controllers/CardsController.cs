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
            
            // Here has a problem
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



    }
}
