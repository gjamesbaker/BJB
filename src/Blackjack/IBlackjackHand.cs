using System.Collections.Generic;

namespace Blackjack
{
    public interface IBlackjackHand
    {
        IEnumerable<IBlackjackCard> Cards { get; }
        void AddCard(IBlackjackCard card);
        int Value();
        bool EligibleForBlackjack { get; set; }
        bool HasBlackjack { get; }
        bool Busted();
        IHandValueCalculator HandValueCalculator { get; set; }
        IBlackjackBet Bet { get; set; }
    }
}