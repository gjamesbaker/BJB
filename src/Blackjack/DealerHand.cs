namespace Blackjack
{
    public class DealerHand : Hand, IDealerHand
    {
        public IBlackjackCard GetHoleCard()
        {
            var cardCount = GetCards().Count;
            return (cardCount == 1 || cardCount == 2) ? GetCards()[0] : null;
        }

        public IBlackjackCard GetFaceUpCard()
        {
            return GetCards().Count == 2 ? GetCards()[1] : null;
        }
    }
}