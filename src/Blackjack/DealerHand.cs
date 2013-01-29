using System.Text;

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

        public override string ToString()
        {
            var output = new StringBuilder();

            output.AppendFormat("   Dealer: ({0})", Value());

            if ( Busted )
                output.Append("   *BUSTED*  ");

            if ( HasBlackjack )
                output.Append("   ** BLACKJACK **  ");

            output.AppendLine().Append("     ");

            foreach (var card in GetCards())
            {
                output.AppendLine(card.ToLongString()).Append("     ");
            }
            output.AppendLine().AppendLine();

            return output.ToString();
        }
    }
}