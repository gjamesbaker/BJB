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

        [Test]
        public void double_down_bet_returns_straight_bet()
        {
            // Arrange
            IBlackjackBet bet = new DoubleDownBet(100);

            // Act
            bet.WinAmount().Should().Equal(100.0);
        }

        [Test]
        public void push_bet_returns_win_amount_of_zero()
        {
            // Arrange
            IBlackjackBet bet = new PushBet();

            // Act
            bet.WinAmount().Should().Equal(0.0);
        }

        [Test]
        public void bet_lose_amount_returns_amount_of_bet()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);

            // Act
            bet.LoseAmount().Should().Equal(100.0);
        }

        [Test]
        public void convert_to_push_bet_returns_a_new_push_bet()
        {
            // Arrange
            IBlackjackBet bet = new AnteBet(100);

            // Act
            IBlackjackBet pushBet = bet.ConvertToPushBet();

            // Assert
            pushBet.Should().Not.Be.Null();
            pushBet.Should().Be.OfType<PushBet>();
            pushBet.Amount.Should().Equal(0.0);
        }

    }
}
