using System.Collections.Generic;

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
        void OfferdoubleDowns();
        void FillPlayerHands();
        void FillDealerHand();
        double SettleBets();
        void ShuffleShoe();
    }
}