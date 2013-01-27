using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class HandTests
    {

        [Test]
        public void can_create_an_empty_hand()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            // Act
            IBlackjackHand hand = new Hand(handValueCalculator);

            // Assert
            hand.Cards.Should().Not.Be.Null();
            hand.Cards.Count().Should().Equal(0);
        }

        [Test]
        public void can_add_a_card_to_an_empty_hand()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var card = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new Hand(handValueCalculator);

            // Act
            hand.AddCard(card);

            // Assert
            hand.Cards.Should().Not.Be.Null();
            hand.Cards.Count().Should().Equal(1);
        }

        [Test]
        public void can_add_a_second_card_to_a_hand()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new Hand(handValueCalculator);

            // Act
            hand.AddCard(card1);
            hand.AddCard(card2);

            // Assert
            hand.Cards.Should().Not.Be.Null();
            hand.Cards.Count().Should().Equal(2);
            hand.Cards.ElementAt(0).Should().Be.SameAs(card1);
            hand.Cards.ElementAt(1).Should().Be.SameAs(card2);
        }

        [Test]
        public void returns_value()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new Hand(handValueCalculator);
            handValueCalculator.Value(hand).Returns(13);

            // Act
            var hardTotal = hand.Value();

            // Assert
            hardTotal.Should().Equal(13);
        }

    }
}

