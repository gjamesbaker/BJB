namespace Blackjack.Bets
{
    public abstract class Bet : IBlackjackBet
    {
        protected Bet(double amount)
        {
            Amount = amount;
        }

        public double Amount { get; private set; }
        public double Odds { get; protected set; }
       
        public virtual double WinAmount()
        {
            return Amount * Odds;
        }

        public virtual double LoseAmount()
        {
            return Amount;
        }

        public PushBet ConvertToPushBet()
        {
            return new PushBet();
        }
    }
}
