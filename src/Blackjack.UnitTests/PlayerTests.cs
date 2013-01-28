using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void place_bet_returns_bet_with_empty_hand()
        {
            // Arrange
            var player = new BlackjackPlayer();

            // Act
            var bet = player.PlaceBet();

            // Assert
            bet.Should().Not.Be.Null();
            bet.Hand.Should().Not.Be.Null();
            bet.Hand.Cards.Count().Should().Equal(0);
        }

        [Test]
        public void place_bet_reduces_balance_by_ante()
        {
            // Arrange
            var player = new BlackjackPlayer {Balance = 1000, Ante = 50};

            // Act
            player.PlaceBet();

            // Assert
            player.Balance.Should().Equal(950);
        }

        [Test]
        public void default_player_accepts_hit_below_17()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var hand = Substitute.For<IBlackjackHand>();
            hand.Value().Returns(16);

            // Act
            var acceptHit = player.Hit(hand);

            // Assert
            acceptHit.Should().Be.True();
        }

        [Test]
        public void default_player_rejects_hit_above_16()
        {
            // Arrange
            var player = new BlackjackPlayer();
            var hand = Substitute.For<IBlackjackHand>();
            hand.Value().Returns(17);

            // Act
            var acceptHit = player.Hit(hand);

            // Assert
            acceptHit.Should().Be.False();
        }
    }
}
