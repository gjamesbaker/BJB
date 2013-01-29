using System.Collections.Generic;
using Blackjack.Cards;
using Blackjack.Hands;

namespace Blackjack
{
    public interface IBlackjackPlayer
    {
        double Ante { get; set; }
        double Balance { get; set; }
        IEnumerable<IPlayerHand> Hands { get; }
        IPlayerHand GetInitialHand();
        
        void StartNewGame();
        void PlaceBet();
        bool OfferSplit(IBlackjackCard dealerFaceUpCard);
        bool OfferDoubleDown(IPlayerHand playerHand, IBlackjackCard dealerFaceUpCard);
        bool Hit(IPlayerHand playerHand, IBlackjackCard dealerFaceUpCard);
        double SettleBet(IPlayerHand hand, IDealerHand dealerHand);
    }
}
