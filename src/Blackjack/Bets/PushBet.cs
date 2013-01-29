using System;

namespace Blackjack.Bets
{
    public class PushBet : Bet
    {
        public PushBet() : base(0)
        {
            Odds = 0;
        }

        public override string ToString()
        {
            return String.Format("PushBet of {0:C}", Amount);
        }
    }
}