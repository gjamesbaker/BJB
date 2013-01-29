namespace Blackjack.Hands
{
    public interface IHandValueCalculator
    {
        int Value(IBlackjackHand hand);
    }
}