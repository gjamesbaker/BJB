using System.Collections.Generic;
using Blackjack.Cards;
using Blackjack.Hands;

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
        bool OfferSplit(IBlackjackCard dealerFaceUpCard);
        bool OfferDoubleDown(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard);
        bool Hit(IBlackjackHand playerHand, IBlackjackCard dealerFaceUpCard);
        double SettleBet(IBlackjackHand hand, IDealerHand dealerHand);
    }
}
