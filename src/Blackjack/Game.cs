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

        public double SettleBets()
        {
            return _table.SettleBets();
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

        public override string ToString()
        {
            var output = new StringBuilder();

            foreach (var player in _table.Players)
            {
                output.AppendLine(player.ToString());
            }

            output.Append(_table.DealerHand.ToString());

            return output.ToString();
        }
        

    }
}
