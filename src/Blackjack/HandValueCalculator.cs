using System.Linq;

namespace Blackjack
{
    public class HandValueCalculator : IHandValueCalculator
    {
        public int Value(IBlackjackHand hand)
        {
            var value = 0;
            var aceCount = 0;

            foreach (var card in hand.Cards)
            {
                value += card.SoftValue;
                if (card is AceCard)
                {
                    aceCount++;
                }
            }

            while ((value > 21) && (aceCount > 0))
            {
                value -= 10;
                aceCount--;
            }
            return value;
        }
    }
}