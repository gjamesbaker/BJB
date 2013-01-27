using System.Collections.Generic;
using CuttingEdge.Conditions;

namespace Blackjack
{
    public class Shoe : IShoe
    {
        private IList<ICard> _cards = new List<ICard>();

        public Shoe(int decks)
        {
            for (var i = 0; i < decks; i++)
            {
                AddDeck(new Deck());
            }
        }

        public Shoe(IDeck deck)
        {
            AddDeck(deck);
        }

        private void AddDeck(IDeck deck)
        {
            foreach (var card in deck.GetCards())
            {
                _cards.Add(card);
            }
        }

        public int CardCount()
        {
            return _cards.Count;
        }

        public ICard Deal()
        {
            if (CardCount() < 1)
                throw new ShoeOutOfCardsException();

            var card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public void Shuffle(IRandom random)
        {
            var newCards = new List<ICard>();
            while (_cards.Count > 0)
            {
                var cardToMove = random.Next(_cards.Count);
                newCards.Add(_cards[cardToMove]);
                _cards.RemoveAt(cardToMove);
            }
            _cards = newCards;
        }
    }
}