﻿using System.Collections.Generic;
using Blackjack.Cards;
using Blackjack.Hands;

namespace Blackjack
{
    public interface IBlackjackTable
    {
        IShoe Shoe { get; }
        IDealerHand DealerHand { get; }
        IEnumerable<IBlackjackPlayer> Players { get; }
        void AddPlayer(IBlackjackPlayer player);
        void StartNewGame();
        void CallForBets();
        void OfferSplits();
        void OfferDoubleDowns();
        void FillPlayerHands();
        void FillDealerHand();
        double SettleBets();
        void ShuffleShoe();
        void DealOneCardToPlayers();
    }
}