using System.Collections.Generic;

namespace Blackjack
{
    public interface IBlackjackPlayer
    {
        double Ante { get; set; }
        double Balance { get; set; }
        IEnumerable<IBlackjackHand> Hands { get; }
        IBlackjackHand GetInitialHand();
        
        void StartNewGame();
        void PlaceBet();
        bool OfferSplit(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard);
        bool OfferDoubleDown(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard);
        bool Hit(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard);
    }
}
