namespace Blackjack
{
    public interface IBlackjackBet
    {
        double Amount { get; }
        double Odds { get; }

        double WinAmount();
        double LoseAmount();
    }
}
