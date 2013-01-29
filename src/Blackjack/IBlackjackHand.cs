using System.Collections.Generic;

namespace Blackjack
{
    public interface IBlackjackHand
    {
        void AddCard(IBlackjackCard card);
        int Value();
        bool EligibleForBlackjack { get; }
        bool HasBlackjack { get; }
        bool Busted { get; }
        bool EligibleForSplit { get; }
        bool EligibleForDoubleDown { get; }
        IHandValueCalculator HandValueCalculator { get; set; }
        IBlackjackBet Bet { get; set; }
        bool CreatedFromSplit { get; set; }
        IList<IBlackjackCard> GetCards();
        void SplitInto(IBlackjackHand hand);
    }
}