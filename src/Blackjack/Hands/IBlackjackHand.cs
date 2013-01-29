using System.Collections.Generic;
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
        IHandValueCalculator HandValueCalculator { get; set; }
        IList<IBlackjackCard> GetCards();
        string ToString();
    }
}