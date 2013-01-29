using System;

namespace Blackjack.Bets
{
    public class DoubleDownBet : Bet
    {
        public DoubleDownBet(double amount)
            : base(amount)
        {
            Odds = 1;
        }

        public override string ToString()
        {
            return String.Format("DoubleDownBet of {0:C} at {1}:1 odds", Amount, Odds);
        }
    }
}