using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class Deck : IDeck
    {
        private readonly List<ICard> _cards;

        public Deck()
        {
            _cards = new List<ICard>();
            for (var suit = 0; suit <= 3; suit++)
            {
                for (var rank = 1; rank <= 13; rank++)
                {
                    _cards.Add(BlackjackCardFactory.Get((Rank) rank, (Suit) suit));
                }
            }
        }

        public IEnumerable<ICard> GetCards()
        {
            return _cards;
        }

        public bool ContainsCard(Rank rank, Suit suit)
        {
            return _cards.Count(c => c.Rank == rank && c.Suit == suit) == 1;
        }
    }
}