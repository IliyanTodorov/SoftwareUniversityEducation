﻿namespace BattleCards.Services
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardViewModel> GetAll();

        int AddCard(AddCardInputModel input);

        void AddCardToUserCollection(string userId, int cardId);

        void RemoveCardFromUserCollection(string userId, int cardId);
    }
}
