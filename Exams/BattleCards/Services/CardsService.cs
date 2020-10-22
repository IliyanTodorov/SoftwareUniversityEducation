namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Cards;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddCard(CardViewModel input, string userId)
        {
            var card = new Card
            {
                Name = input.Name,
                ImageUrl = input.ImageURL,
                Keyword = input.Keyword,
                Description = input.Description,
                Attack = input.Attack,
                Health = input.Health
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
                UserId = userId,
                CardId = cardId
            });

            this.dbContext.SaveChanges();
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

        public IEnumerable<CardViewModel> GetUserCollection(string userId)
        {
            return this.dbContext.UserCards.Where(x => x.UserId == userId)
                .Select(x => new CardViewModel
                {
                    Attack = x.Card.Attack,
                    Description = x.Card.Description,
                    Health = x.Card.Health,
                    ImageURL = x.Card.ImageUrl,
                    Name = x.Card.Name,
                    Keyword = x.Card.Keyword,
                    Id = x.CardId,
                }).ToList();
        }
    }
}
