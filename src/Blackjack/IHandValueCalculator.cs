namespace Blackjack
{
    public interface IHandValueCalculator
    {
        int Value(IBlackjackHand hand);
    }
}