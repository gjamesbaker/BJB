using System.Linq;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class DeckTests
    {

        [Test]
        public void base_deck_contains_all_52_cards()
        {
            // Arrange
            IDeck deck = new Deck();

            // Act
            // Assert
            for (var suit = 0; suit < 4; suit++)
            {
                for (var rank = 1; rank < 14; rank++)
                {
                    var r = (Rank) rank;
                    var s = (Suit) suit;

                    deck.ContainsCard(r, s).Should().Be.True();
                }
            }
        }

        [Test]
        public void can_access_deck_cards()
        {
            // Arrange
            IDeck deck = new Deck();

            // Act
            var cards = deck.GetCards();

            // Assert
            cards.Count().Should().Equal(52);
        }

    }
}

