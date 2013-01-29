using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    public class BlackjackTable : IBlackjackTable
    {
        private readonly List<IBlackjackPlayer> _players = new List<IBlackjackPlayer>();

        public BlackjackTable()
        {
            DealerHand = new DealerHand();
            Shoe = new Shoe(6);
        }

        public IShoe Shoe { get; private set; }

        public IDealerHand DealerHand { get; private set; }

        public IEnumerable<IBlackjackPlayer> Players
        {
            get { return _players; }
        }

        public void AddPlayer(IBlackjackPlayer player)
        {
            _players.Add(player);
        }

        public void StartNewGame()
        {
            foreach (var player in Players)
            {
                player.StartNewGame();
            }
            DealerHand = new DealerHand();
        }

        // TODO: Add Test
        public void CallForBets()
        {
            foreach (var player in Players)
            {
                player.PlaceBet();
            }
        }

        // TODO: Add Test
        public void OfferSplits()
        {
            foreach (var player in Players)
            {
                bool splitOccurred;
                var dealerFaceUpCard = DealerHand.GetFaceUpCard();

                do
                {
                    splitOccurred = false;

                    foreach (var hand in player.Hands.ToList())
                    {
                        if (hand.EligibleForSplit)
                        {
                            splitOccurred = splitOccurred || player.OfferSplit(hand, dealerFaceUpCard);
                        }
                    }

                    if (splitOccurred)
                        CompleteHands(player);

                } while (splitOccurred);
            }
        }

        // TODO: Add Test
        public void OfferdoubleDowns()
        {
            var dealerFaceUpCard = DealerHand.GetFaceUpCard();

            foreach (var hand in 
                         from player in Players 
                                from hand in player.Hands 
                                where hand.EligibleForDoubleDown
                                where player.OfferDoubleDown(hand, dealerFaceUpCard) 
                     select hand)
            {
                hand.AddCard(Shoe.Deal());
            }
        }

        // TODO: Add Test
        public void FillPlayerHands()
        {
            foreach (var player in Players)
            {
                foreach (var hand in player.Hands)
                {
                    while(player.Hit(hand, DealerHand.GetFaceUpCard()))
                        hand.AddCard(Shoe.Deal());
                }
            }
        }

        // TODO: Add Test
        public void FillDealerHand()
        {
            while(DealerHand.Value()<17)
                DealerHand.AddCard(Shoe.Deal());
        }

        private void CompleteHands(IBlackjackPlayer player)
        {
            foreach (var hand in player.Hands.Where(hand => hand.GetCards().Count == 1))
            {
                hand.AddCard(Shoe.Deal());
            }
        }

    }
}
