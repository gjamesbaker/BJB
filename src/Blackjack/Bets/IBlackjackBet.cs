﻿namespace Blackjack.Bets
{
    public interface IBlackjackBet
    {
        double Amount { get; }
        double Odds { get; }

        double WinAmount();
        double LoseAmount();
        PushBet ConvertToPushBet();
    }
}
