namespace Blackjack
{
    public class HandValueCalculator : IHandValueCalculator
    {
        public int Value(IBlackjackHand hand)
        {
            var handValue = 0;
            var aceCount = 0;

            foreach (var card in hand.Cards)
            {
                handValue += card.Value;
                if (card is IAceCard)
                {
                    aceCount++;
                }
            }

            while ((handValue > 21) && (aceCount > 0))
            {
                handValue -= 10;
                aceCount--;
            }
            return handValue;
        }
    }
}