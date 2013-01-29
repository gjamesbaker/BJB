using Blackjack.Cards;

namespace Blackjack.Hands
{
    public interface IDealerHand : IBlackjackHand
    {
        IBlackjackCard GetFaceUpCard();
    }
}