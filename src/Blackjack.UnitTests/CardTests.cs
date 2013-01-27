using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void card_factory_returns_correct_card()
        {
            // Act
            var card = BlackjackCardFactory.Get(Rank.Ace, Suit.Clubs);

            // Assert
            card.Rank.Should().Equal(Rank.Ace);
            card.Suit.Should().Equal(Suit.Clubs);
        }

        [Test]
        public void ace_has_two_values()
        {
            // Act
            var ace = BlackjackCardFactory.Get(Rank.Ace, Suit.Clubs);

            // Assert
            ace.Value.Should().Equal(11);
        }

        [Test]
        public void face_cards_have_values_of_ten()
        {
            // Act
            var jack = BlackjackCardFactory.Get(Rank.Jack, Suit.Diamonds);

            // Assert
            jack.Value.Should().Equal(10);
        }

        [Test]
        public void ordinal_cards_have_values_of_rank()
        {
            // Act
            var five = BlackjackCardFactory.Get(Rank.Five, Suit.Hearts);

            // Assert
            five.Value.Should().Equal(5);
        }

    }
}
