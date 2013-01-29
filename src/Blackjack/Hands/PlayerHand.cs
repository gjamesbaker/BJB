using Blackjack.Cards;

namespace Blackjack.Hands
{
    public class PlayerHand : Hand, IPlayerHand
    {
        public PlayerHand()
        {
            EligibleForDoubleDown = false;
            EligibleForSplit = false;
        }

        public bool EligibleForSplit { get; private set; }

        public bool EligibleForDoubleDown { get; private set; }

        public bool CreatedFromSplit { get; set; }

        public override void AddCard(IBlackjackCard card)
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

        public void SplitInto(IPlayerHand hand)
        {
            var card2 = _cards[1];
            _cards.RemoveAt(1);
            hand.AddCard(card2);

            CreatedFromSplit = true;
            hand.CreatedFromSplit = true;
        }

    }
}