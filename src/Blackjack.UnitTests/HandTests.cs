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
            var player = Substitute.For<IBlackjackPlayer>();

            // Act
            IBlackjackHand hand = new PlayerHand(player);

            // Assert
            hand.Cards.Should().Not.Be.Null();
            hand.Cards.Count().Should().Equal(0);
        }

        [Test]
        public void can_add_a_card_to_an_empty_hand()
        {
            // Arrange
            var player = Substitute.For<IBlackjackPlayer>();
            var card = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new PlayerHand(player);

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
            var player = Substitute.For<IBlackjackPlayer>();
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new PlayerHand(player);

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
            var player = Substitute.For<IBlackjackPlayer>();
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand(player);
            handValueCalculator.Value(hand).Returns(13);

            hand.HandValueCalculator = handValueCalculator;
            
            // Act
            var hardTotal = hand.Value();

            // Assert
            hardTotal.Should().Equal(13);
        }

        [Test]
        public void hand_does_not_return_blackjack_for_any_cards_totaling_less_than_21()
        {
            // Arrange
            var player = Substitute.For<IBlackjackPlayer>();
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var argHand = Substitute.For<IBlackjackHand>();
            
            handValueCalculator.Value(argHand).ReturnsForAnyArgs(10, 19); 
            
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();

            IBlackjackHand hand = new PlayerHand(player);

            // Act
            hand.AddCard(card1);
            // Assert
            hand.HasBlackjack.Should().Be.False();
            hand.EligibleForBlackjack.Should().Be.True();

            // Act
            hand.AddCard(card2);
            // Assert
            hand.HasBlackjack.Should().Be.False();
            hand.EligibleForBlackjack.Should().Be.False();
        }

        [Test]
        public void hand_does_not_return_blackjack_for_3_cards_totaling_21()
        {
            // Arrange
            var player = Substitute.For<IBlackjackPlayer>();
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var argHand = Substitute.For<IBlackjackHand>();

            handValueCalculator.Value(argHand).ReturnsForAnyArgs(9, 15, 21);

            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            var card3 = Substitute.For<IBlackjackCard>();

            IBlackjackHand hand = new PlayerHand(player);

            // Act
            hand.AddCard(card1);
            hand.EligibleForBlackjack.Should().Be.True();
            hand.AddCard(card2);
            hand.EligibleForBlackjack.Should().Be.False();
            hand.AddCard(card3);
            
            // Assert
            hand.HasBlackjack.Should().Be.False();
            hand.EligibleForBlackjack.Should().Be.False();
        }
     
        [Test]
        public void hand_shows_busted_when_value_over_21()
        {
            // Arrange
            var player = Substitute.For<IBlackjackPlayer>();
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand(player);
            handValueCalculator.Value(hand).Returns(22);

            hand.HandValueCalculator = handValueCalculator;

            // Act
            var busted = hand.Busted();

            // Assert
            busted.Should().Be.True();
        }

        [Test]
        public void hand_shows_not_busted_when_value_under_22()
        {
            // Arrange
            var player = Substitute.For<IBlackjackPlayer>();
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand(player);
            handValueCalculator.Value(hand).Returns(20);

            hand.HandValueCalculator = handValueCalculator;

            // Act
            var busted = hand.Busted();

            // Assert
            busted.Should().Be.False();
        }

    }
}

