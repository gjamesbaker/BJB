using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Cards;
using Blackjack.Hands;

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

        public void CallForBets()
        {
            foreach (var player in Players)
            {
                player.PlaceBet();
            }
        }

        public void OfferSplits()
        {
            var dealerFaceUpCard = DealerHand.GetFaceUpCard();

            foreach (var player in Players)
            {
                bool splitOccurred;
                do
                {
                    splitOccurred = player.OfferSplit(dealerFaceUpCard);

                    // If a split occurred, we need to add the second card to the split hands
                    if (splitOccurred)
                        CompleteHands(player);
                } while (splitOccurred);  // Keep going until no more splits are possible or accepted.
            }
        }

        public void OfferDoubleDowns()
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

        public void FillDealerHand()
        {
            while(DealerHand.Value()<17)
                DealerHand.AddCard(Shoe.Deal());
        }

        public double SettleBets()
        {
            return Players.Sum(player => player.Hands.Sum(hand => player.SettleBet(hand, DealerHand)));
        }

        public void ShuffleShoe()
        {
            Shoe.Shuffle();
        }

        private void CompleteHands(IBlackjackPlayer player)
        {
            foreach (var hand in player.Hands.Where(hand => hand.GetCards().Count == 1))
            {
                hand.AddCard(Shoe.Deal());
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            foreach (var player in Players)
            {
                output.AppendLine(player.ToString());
            }

            output.Append(DealerHand.ToString());

            return output.ToString();
        }

    }
}
