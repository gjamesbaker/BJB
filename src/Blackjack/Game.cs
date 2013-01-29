using System.Linq;
using System.Text;

namespace Blackjack
{
    public class Game
    {
        private readonly IBlackjackTable _table;

        public Game(IBlackjackTable table)
        {
            _table = table;
        }

        
        // TODO: Implement a state pattern with signature like "public CurrentState NextStep()"

        public void StartNewGame()
        {
            _table.StartNewGame();
        }

        public void DealHands()
        {
            DealOneCardToPlayers();
            DealOneCardToDealer();

            DealOneCardToPlayers();
            DealOneCardToDealer();
        }

        public void CallForBets()
        {
            _table.CallForBets();
        }

        public void OfferSplits()
        {
            _table.OfferSplits();
        }

        public void OfferDoubleDowns()
        {
            _table.OfferdoubleDowns();
        }

        public void FillPlayerHands()
        {
            _table.FillPlayerHands();
        }

        public void FillDealerHand()
        {
            _table.FillDealerHand();
        }

        private void DealOneCardToDealer()
        {
            _table.DealerHand.AddCard(_table.Shoe.Deal());
        }

        private void DealOneCardToPlayers()
        {
            // TODO: move this down to Table
            foreach (var player in _table.Players)
            {
                player.GetInitialHand().AddCard(_table.Shoe.Deal());
            }
        }

        public string GetHandInfo()
        {
            var output = new StringBuilder();

            foreach (var hand in _table.Players.SelectMany(player => player.Hands))
            {
                output.Append("     Hand: ").Append(hand.Value()).Append("   ").Append(hand.Bet);

                if (hand.Busted)
                    output.AppendLine("   *BUSTED*  ").Append("          ");
                else if(hand.HasBlackjack)
                    output.AppendLine("   ** BLACKJACK **  ").Append("          ");
                else
                    output.AppendLine().Append("          ");

                foreach (var card in hand.GetCards())
                {
                    output.Append(card.ToLongString()).Append("    ");
                }
                output.AppendLine().AppendLine();
            }

            output.Append("     Dealer: ").Append(_table.DealerHand.Value());

            // TODO: implement a _table.DealerBusted method
            if (_table.DealerHand.Busted)
                output.AppendLine("   *BUSTED*  ").Append("          ");
            else if (_table.DealerHand.HasBlackjack)
                output.AppendLine("   ** BLACKJACK **  ").Append("          ");
            else
                output.AppendLine().Append("          ");


            foreach (var card in _table.DealerHand.GetCards())
            {
                output.Append(card.ToLongString()).Append("  ");
            }
            output.AppendLine().AppendLine();

            return output.ToString();
        }



    }
}
