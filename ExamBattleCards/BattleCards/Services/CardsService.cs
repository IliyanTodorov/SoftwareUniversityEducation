namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Models;
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

        public int AddCard(string name, string imageURL, string keyword, int attack, int health, string description)
        {
            var card = new Card
            {
                Name = name,
                ImageUrl = imageURL,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            };

            this.dbContext.Cards.Add(card);
            this.dbContext.SaveChanges();

            return card.Id;
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (this.dbContext.UserCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }

            this.dbContext.UserCards.Add(new UserCard
            {
                CardId = cardId,
                UserId = userId,
            });

            this.dbContext.SaveChanges();
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return this.dbContext.Cards.Select(x => new CardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Attack = x.Attack,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.Keyword,
            }).ToList();
        }

        public void RemoveCardFromUserCollection(int cardId)
        {
            throw new System.NotImplementedException();
        }
    }
}
