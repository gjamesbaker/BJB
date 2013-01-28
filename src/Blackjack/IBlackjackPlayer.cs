using System.Collections.Generic;

namespace Blackjack
{
    public interface IBlackjackPlayer
    {
        int Ante { get; set; }
        int Balance { get; set; }
        IEnumerable<IBlackjackHand> Hands { get; }
        
        void NewGame();
        IBlackjackBet PlaceBet();
        bool Hit(IBlackjackHand hand);
    }
}
