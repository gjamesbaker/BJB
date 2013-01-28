using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackPlayer : IBlackjackPlayer
    {
        private List<IBlackjackHand> _hands;
        
        public BlackjackPlayer()
        {
            Balance = 0;
            Ante = 10;
        }

        public int Ante { get; set; }
        public int Balance { get; set; }
        
        public IEnumerable<IBlackjackHand> Hands
        {
            get { return _hands; }
        }

        public void NewGame()
        {
            _hands = new List<IBlackjackHand>();
        }

        public IBlackjackBet PlaceBet()
        {
            var hand = new PlayerHand(this);
            Balance -= Ante;
            return new Bet(Ante, hand);
        }

        public bool Hit(IBlackjackHand hand)
        {
            return hand.Value() < 17;
        }
    }
}