using System.Text;
using Blackjack.Cards;

namespace Blackjack.Hands
{
    public class DealerHand : Hand, IDealerHand
    {
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
                output.AppendLine(card.ToString()).Append("     ");
            }
            output.AppendLine().AppendLine();

            return output.ToString();
        }
    }
}