namespace Blackjack
{
    public class Game
    {
        private readonly IBlackjackTable _table;

        public Game(IBlackjackTable table)
        {
            _table = table;
        }

        
        // TODO: Implement a state pattern with signature like "public IGameState NextStep()"

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
            _table.OfferDoubleDowns();
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
            _table.DealOneCardToPlayers();
        }

        public override string ToString()
        {
            return _table.ToString();
        }
        

    }
}
