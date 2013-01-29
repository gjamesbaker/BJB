using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public abstract class Hand : IBlackjackHand
    {
        private readonly List<IBlackjackCard> _cards;
        
        protected Hand()
        {
            _cards = new List<IBlackjackCard>();

            EligibleForBlackjack = true;
            EligibleForDoubleDown = false;
            EligibleForSplit = false;

            HandValueCalculator = new HandValueCalculator();
        }

        public IBlackjackBet Bet { get; set; }

        public bool EligibleForBlackjack { get; private set; }

        public bool EligibleForSplit { get; private set; }

        public bool EligibleForDoubleDown { get; private set; }

        public bool CreatedFromSplit { get; set; }

        public IHandValueCalculator HandValueCalculator { get; set; }

        public bool HasBlackjack { get; private set; }

        public void AddCard(IBlackjackCard card)
        {
            _cards.Add(card);

            if (CreatedFromSplit) EligibleForBlackjack = false;
            
            if (EligibleForBlackjack)
                CheckForBlackjack();

            CheckForSplitEligibility();

            CheckForDoubleDownEligibility();
        }

        private void CheckForSplitEligibility()
        {
            EligibleForSplit = (_cards.Count == 2 && _cards[0].Value == _cards[1].Value);
        }

        private void CheckForDoubleDownEligibility()
        {
            // House rules: Double Down only allowed on initial hand worth 9, 10, 11
            EligibleForDoubleDown = (!CreatedFromSplit && _cards.Count == 2 && Value() > 8 && Value() < 12);
        }

        public IList<IBlackjackCard> GetCards()
        {
            return _cards;
        }

        public void SplitInto(IBlackjackHand hand)
        {
            var card2 = _cards[1];
            _cards.RemoveAt(1);
            hand.AddCard(card2);

            CreatedFromSplit = true;
            hand.CreatedFromSplit = true;
        }

        public bool Busted
        {
            get { return Value() > 21; }
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