using BattleCards.Data;
using BattleCards.Models;
namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddCard(AddCardInputModel input)
        {
            var card = new Card
            {
                Attack = input.Attack,
                Health = input.Health,
                Description = input.Description,
                Name = input.Name,
                ImageUrl = input.Image,
                Keyword = input.Keyword,
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();
            return card.Id;
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return this.db.Cards.Select(x => new CardViewModel
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

        public IEnumerable<CardViewModel> GetByUserId(string userId)
        {
            throw new System.NotImplementedException();
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            throw new System.NotImplementedException();
        }
    }
}