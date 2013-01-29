using System.Linq;
using Blackjack.Cards;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void table_has_a_shoe_of_cards()
        {
            // Arrange
            var table = new BlackjackTable();

            // Act
            var shoe = table.Shoe;

            // Assert
            shoe.Should().Not.Be.Null();
            shoe.CardCount().Should().Not.Equal(0);
        }

        [Test]
        public void table_has_a_dealer_hand()
        {
            // Arrange
            var table = new BlackjackTable();

            // Act
            var dealerHand = table.DealerHand;

            // Assert
            dealerHand.Should().Not.Be.Null();
            dealerHand.GetCards().Should().Not.Be.Null();
        }

        [Test]
        public void can_add_players_to_table()
        {
            // Arrange
            var table = new BlackjackTable();
            var player = Substitute.For<IBlackjackPlayer>();

            // Act
            table.AddPlayer(player);

            // Assert
            table.Players.Count().Should().Equal(1);
            table.Players.ElementAt(0).Should().Be.SameAs(player);
        }

        [Test]
        public void start_new_game_resets_dealers_hand()
        {
            // Arrange
            var table = new BlackjackTable();
            var hand = table.DealerHand;
            var card = Substitute.For<IBlackjackCard>();
            hand.AddCard(card);
            hand.GetCards().Count().Should().Equal(1);

            // Act
            table.StartNewGame();

            // Assert
            table.DealerHand.GetCards().Count().Should().Equal(0);
        }

        [Test]
        public void start_new_game_calls_new_game_on_each_player()
        {
            // Arrange
            var table = new BlackjackTable();
            var player1 = Substitute.For<IBlackjackPlayer>();
            var player2 = Substitute.For<IBlackjackPlayer>();

            table.AddPlayer(player1);
            table.AddPlayer(player2);

            // Act
            table.StartNewGame();

            // Assert
            player1.Received().StartNewGame();
            player2.Received().StartNewGame();
        }
    }
}
