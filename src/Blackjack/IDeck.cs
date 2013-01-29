using System.Collections.Generic;
using Blackjack.Cards;

namespace Blackjack
{
    public interface IDeck
    {
        bool ContainsCard(Rank rank, Suit suit);
        IEnumerable<IBlackjackCard> GetCards();
    }
}
