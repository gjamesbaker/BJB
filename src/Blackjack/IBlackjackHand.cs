using System.Collections.Generic;

namespace Blackjack
{
    public interface IBlackjackHand
    {
        IEnumerable<IBlackjackCard> Cards { get; }
        void AddCard(IBlackjackCard card);
        int Value();
    }
}