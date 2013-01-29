using System.Collections.Generic;
using Blackjack.Bets;
using Blackjack.Cards;

namespace Blackjack.Hands
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
        string ToString();
    }
}