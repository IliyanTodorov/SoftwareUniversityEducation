namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAll();

        int AddCard(string name, string imageURL, string keyword, int attack, int health, string description);

        void AddCardToUserCollection(string userId, int cardId);

        void RemoveCardFromUserCollection(int cardId);
    }
}
