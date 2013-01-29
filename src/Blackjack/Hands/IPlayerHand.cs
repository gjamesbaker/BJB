using Blackjack.Bets;

namespace Blackjack.Hands
{
    public interface IPlayerHand : IBlackjackHand
    {
        IBlackjackBet Bet { get; set; }
        bool EligibleForSplit { get; }
        bool EligibleForDoubleDown { get; }
        bool CreatedFromSplit { get; set; }
        void SplitInto(IPlayerHand hand);
    }
}