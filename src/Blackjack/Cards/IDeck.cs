using System.Collections.Generic;

namespace Blackjack.Cards
{
    public interface IDeck
    {
        bool ContainsCard(Rank rank, Suit suit);
        IEnumerable<IBlackjackCard> GetCards();
    }
}
