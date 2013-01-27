using System.Collections.Generic;
using CuttingEdge.Conditions;

namespace Blackjack
{
    public class Hand : IBlackjackHand
    {
        private readonly IHandValueCalculator _handValueCalculator;
        private readonly List<IBlackjackCard> _cards;

        public Hand(IHandValueCalculator handValueCalculator)
        {
            Condition.Requires(handValueCalculator, "handValueCalculator").IsNotNull();

            _handValueCalculator = handValueCalculator;
            _cards = new List<IBlackjackCard>();
        }

        public IEnumerable<IBlackjackCard> Cards
        {
            get { return _cards; }
        }

        public void AddCard(IBlackjackCard card)
        {
            _cards.Add(card);
        }

        public int Value()
        {
            return _handValueCalculator.Value(this);
        }

    }
}