using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackPlayer : IBlackjackPlayer
    {
        private List<IBlackjackHand> _hands = new List<IBlackjackHand>();
        
        public BlackjackPlayer()
        {
            Balance = 0;
            Ante = 10;
        }

        public double Ante { get; set; }
        public double Balance { get; set; }
        
        public IEnumerable<IBlackjackHand> Hands
        {
            get { return _hands; }
        }

        // TODO: Add Test
        public IBlackjackHand GetInitialHand()
        {
            if (_hands.Count.Equals(0))
            {
                throw new MissingBetException();
            }
            return _hands[0];
        }

        public void StartNewGame()
        {
            _hands = new List<IBlackjackHand>();
        }

        public void PlaceBet()
        {
            var hand = new PlayerHand();
            Balance -= Ante;
            hand.Bet = new AnteBet(Ante);
            _hands.Add(hand);
        }

        public bool OfferSplit(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard)
        {
            if (!playerHand.EligibleForSplit)
            {
                return false;
            }
            
            var hand = new PlayerHand();
            Balance -= Ante;
            hand.Bet = new AnteBet(Ante);
            _hands.Add(hand);

            playerHand.SplitInto(hand);

            return true;
        }

        public bool OfferDoubleDown(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard)
        {
            // Simple Strategy - always accept if eligible
            if (!playerHand.EligibleForDoubleDown)
            {
                return false;
            }

            var originalAmount = playerHand.Bet.Amount;

            Balance -= originalAmount;

            playerHand.Bet = new DoubleDownBet(originalAmount * 2);

            return true;
        }

        public bool Hit(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard)
        {
            return playerHand.Value() < 17 && playerHand.Bet is AnteBet;
        }
    }
}