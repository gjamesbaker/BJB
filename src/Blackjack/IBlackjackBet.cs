namespace Blackjack
{
    public interface IBlackjackBet
    {
        int Amount { get; }
        IBlackjackHand Hand { get; }

        int WinAmount();
        int LoseAmount();
    }
}
