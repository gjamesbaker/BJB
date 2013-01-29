using Blackjack.Bets;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class BetTests
    {
        [Test]
        public void non_blackjack_win_returns_straight_bet()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);

            // Act
            bet.WinAmount().Should().Equal(100.0);
        }

        [Test]
        public void blackjack_win_returns_150_pct()
        {
            // Arrange
            IBlackjackBet bet = new BlackjackBet(100);

            // Act
            bet.WinAmount().Should().Equal(150.0);
        }

    }
}
