using System.Collections.Generic;

namespace Blackjack
{
    public class BlackjackTable
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
    }
}
