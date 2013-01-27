using System.Collections.Generic;

namespace Blackjack
{
    public interface IDeck
    {
        bool ContainsCard(Rank rank, Suit suit);
        IEnumerable<ICard> GetCards();
    }
}
