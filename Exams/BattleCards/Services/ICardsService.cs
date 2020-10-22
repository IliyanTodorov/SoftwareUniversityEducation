namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAll();

        IEnumerable<CardViewModel> GetUserCollection(string userId);

        int AddCard(AddCardInputModel input);

        void AddCardToUserCollection(string userId, int cardId);
    }
}
