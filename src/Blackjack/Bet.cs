using System;

namespace Blackjack
{
    public abstract class Bet : IBlackjackBet
    {
        protected Bet(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; private set; }
        public double Odds { get; protected set; }
       
        public double WinAmount()
        {
            return Amount * Odds;
        }

        public double LoseAmount()
        {
            return Amount;
        }
    }

    public class AnteBet : Bet
    {
        public AnteBet(double amount)
            : base(amount)
        {
            Odds = 1;
        }

        public BlackjackBet ConvertToBlackjackBet()
        {
            return new BlackjackBet(Amount);
        }

        public DoubleDownBet ConvertToDoubledownBet()
        {
            return new DoubleDownBet(Amount * 2.0);
        }

        public override string ToString()
        {
            return String.Format("AnteBet of {0:C} at {1}:1 odds", Amount, Odds);
        }
    }

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
