using System.Collections.Generic;
using System.Text;
using Blackjack.Bets;
using Blackjack.Cards;

namespace Blackjack.Hands
{
    public abstract class Hand : IBlackjackHand
    {
        protected readonly List<IBlackjackCard> _cards;
        
        protected Hand()
        {
            _cards = new List<IBlackjackCard>();
            EligibleForBlackjack = true;
            HandValueCalculator = new HandValueCalculator();
        }

        public IBlackjackBet Bet { get; set; }

        public bool EligibleForBlackjack { get; protected set; }

        public IHandValueCalculator HandValueCalculator { get; set; }

        public bool HasBlackjack { get; private set; }

        public virtual void AddCard(IBlackjackCard card)
        {
            _cards.Add(card);

            if (EligibleForBlackjack)
                CheckForBlackjack();
        }


        public IList<IBlackjackCard> GetCards()
        {
            return _cards;
        }


        public bool Busted
        {
            get { return Value() > 21; }
        }

        protected void CheckForBlackjack()
        {
            switch (_cards.Count)
            {
                case 0:
                case 1:
                    return;
                case 2:
                    HasBlackjack = EligibleForBlackjack && Value() == 21;
                    EligibleForBlackjack = HasBlackjack;
                    if (HasBlackjack) SwapBet();
                    return;
                default:
                    HasBlackjack = EligibleForBlackjack = false;
                    return;
            }
        }

        private void SwapBet()
        {
            if (Bet is AnteBet)
                Bet = new BlackjackBet(Bet.Amount);
        }

        public int Value()
        {
            return HandValueCalculator.Value(this);
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.AppendFormat("   Hand:  ({0})   Bet: ", Value()).Append(Bet);
            if (Busted)
                output.AppendLine("    BUSTED");
            else if (HasBlackjack)
                output.AppendLine("    BLACKJACK");
            else output.AppendLine();

            foreach (var card in _cards)
            {
                output.Append("     ").AppendLine(card.ToString());
            }

            return output.ToString();
        }
    }

}