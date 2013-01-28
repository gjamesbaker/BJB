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
            var hand = Substitute.For<IBlackjackHand>();
            hand.HasBlackjack.Returns(false);
            IBlackjackBet bet = new Bet(100, hand);

            // Act
            bet.WinAmount().Should().Equal(100);
        }

        [Test]
        public void blackjack_win_returns_150_pct()
        {
            // Arrange
            var hand = Substitute.For<IBlackjackHand>();
            hand.HasBlackjack.Returns(true);
            IBlackjackBet bet = new Bet(100, hand);

            // Act
            bet.WinAmount().Should().Equal(150);
        }

    }
}
