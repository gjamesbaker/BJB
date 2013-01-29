using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool OfferSplit(IBlackjackCard dealerFaceUpCard)
        {
            var splitOccurred = false;

            // Offer a split to all eligible hands
            foreach (var hand in Hands.ToList())
            {
                if (hand.EligibleForSplit)
                {
                    splitOccurred = splitOccurred || ConsiderSplitOffer(hand, dealerFaceUpCard);
                }
            }
            return splitOccurred;
        }

        private bool ConsiderSplitOffer(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard)
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
            // Simplistic strategy
            return playerHand.Value() < 17 && playerHand.Bet is AnteBet;
        }

        // TODO: Add Test
        public double SettleBet(IBlackjackHand hand, IDealerHand dealerHand)
        {
            if (hand.Busted)
                return hand.Bet.LoseAmount();

            if (hand.Value() == dealerHand.Value())
            {
                Balance += hand.Bet.Amount;
                hand.Bet = hand.Bet.ConvertToPushBet();

                return 0;
            }

            if (hand.Value() < dealerHand.Value())
            {
                return hand.Bet.LoseAmount();
            }
            
            Balance += hand.Bet.Amount + hand.Bet.WinAmount();
            return hand.Bet.WinAmount()*-1;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendFormat("Balance: {0:C}  Hands: {1}", Balance, _hands.Count).AppendLine();

            foreach (var hand in _hands)
            {
                output.AppendLine(hand.ToString());
            }

            return output.ToString();
        }
    }
}