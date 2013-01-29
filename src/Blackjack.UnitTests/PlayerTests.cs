using System.Linq;
using Blackjack.Bets;
using Blackjack.Cards;
using Blackjack.Hands;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void place_bet_reduces_balance_by_ante()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer {Balance = 1000, Ante = 50};

            // Act
            player.PlaceBet();

            // Assert
            player.Balance.Should().Equal(950.0);
        }

        [Test]
        public void default_player_accepts_hit_below_17()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();
            var hand = Substitute.For<IPlayerHand>();
            var bet = Substitute.For<AnteBet>(0.0);

            hand.Value().Returns(16);
            hand.Bet.Returns(bet);

            // Act
            var acceptHit = player.Hit(hand, dealerFaceUpCard);

            // Assert
            acceptHit.Should().Be.True();
        }

        [Test]
        public void default_player_rejects_hit_above_16()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            var dealerFaceUpCard = Substitute.For<IBlackjackCard>();
            var hand = Substitute.For<IPlayerHand>();
            hand.Value().Returns(17);

            // Act
            var acceptHit = player.Hit(hand, dealerFaceUpCard);

            // Assert
            acceptHit.Should().Be.False();
        }

        [Test]
        public void start_new_game_resets_hands_to_empty()
        {
            // Arrange
            IBlackjackPlayer player = new BlackjackPlayer();
            player.PlaceBet();
            player.Hands.Count().Should().Equal(1);

            // Act
            player.StartNewGame();

            // Assert
            player.Hands.Count().Should().Equal(0);
        }

    }
}
