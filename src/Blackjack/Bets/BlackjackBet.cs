using System;

namespace Blackjack.Bets
{
    public class BlackjackBet : Bet
    {
        public BlackjackBet(double amount)
            : base(amount)
        {
            Odds = 1.5;
        }

        public override string ToString()
        {
            return String.Format("BlackjackBet of {0:C} at {1}:1 odds", Amount, Odds);
        }
    }
}