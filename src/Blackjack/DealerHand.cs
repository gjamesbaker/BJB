namespace Blackjack
{
    public class DealerHand : Hand, IDealerHand
    {
        public IBlackjackCard GetFaceUpCard()
        {
            return _cards.Count == 2 ? _cards[1] : null;
        }
    }
}