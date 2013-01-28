using System.Collections.Generic;

namespace Blackjack
{
    public abstract class Hand : IBlackjackHand
    {
        protected readonly List<IBlackjackCard> _cards;

        protected Hand()
        {
            _cards = new List<IBlackjackCard>();

            EligibleForBlackjack = true;
            HasBlackjack = false;

            HandValueCalculator = new HandValueCalculator();
        }

        public IBlackjackBet Bet { get; set; }
        
        public IEnumerable<IBlackjackCard> Cards { get { return _cards; } }

        public bool EligibleForBlackjack { get; set; }

        public IHandValueCalculator HandValueCalculator { get; set; }

        public bool HasBlackjack { get; private set; }

        public IBlackjackPlayer Player { get; protected set; }


        public void AddCard(IBlackjackCard card)
        {
            _cards.Add(card);
            if (EligibleForBlackjack)
                CheckForBlackjack();
        }

        public bool Busted()
        {
            return Value() > 21;
        }

        private void CheckForBlackjack()
        {
            switch (_cards.Count)
            {
                case 0:
                case 1:
                    return;
                case 2:
                    HasBlackjack = EligibleForBlackjack && Value() == 21;
                    EligibleForBlackjack = HasBlackjack;
                    return;
                default:
                    HasBlackjack = false;
                    EligibleForBlackjack = false;
                    return;
            }
        }

        public int Value()
        {
            return HandValueCalculator.Value(this);
        }
    }
}