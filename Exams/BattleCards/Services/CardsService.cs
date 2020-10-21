namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            var allCardsInputModel = this.dbContext.Cards.Select(c => new CardViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Keyword = c.Keyword,
                ImageURL = c.ImageUrl,
                Attack = c.Attack,
                Health = c.Health,
                Description = c.Description
            }).ToList();

            return allCardsInputModel;
        }
    }
}
