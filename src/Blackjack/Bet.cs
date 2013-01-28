using System;

namespace Blackjack
{
    public class Bet : IBlackjackBet
    {
        public Bet(int amount, IBlackjackHand hand)
        {
            Hand = hand;
            Amount = amount;
        }

        public int Amount { get; private set; }
        public IBlackjackHand Hand { get; private set; }
        
        public int WinAmount()
        {
            return Hand.HasBlackjack ? Amount*15/10 : Amount;
        }

        public int LoseAmount()
        {
            return Amount;
        }
    }
}
