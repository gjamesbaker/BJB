namespace Blackjack
{
    public interface IDealerHand : IBlackjackHand
    {
        IBlackjackCard GetFaceUpCard();
    }
}