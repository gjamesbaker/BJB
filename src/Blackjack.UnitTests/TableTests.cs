using System.Linq;
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
            dealerHand.Cards.Should().Not.Be.Null();
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
    }
}
