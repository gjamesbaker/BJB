using System;

namespace Blackjack.Bets
{
    public class AnteBet : Bet
    {
        public AnteBet(double amount)
            : base(amount)
        {
            Odds = 1;
        }

        public override string ToString()
        {
            return String.Format("AnteBet of {0:C} at {1}:1 odds", Amount, Odds);
        }
    }
}