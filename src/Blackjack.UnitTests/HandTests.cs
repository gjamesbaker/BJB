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
    public class HandTests
    {

        [Test]
        public void can_create_an_empty_hand()
        {
            // Arrange

            // Act
            IBlackjackHand hand = new PlayerHand();

            // Assert
            hand.GetCards().Should().Not.Be.Null();
            hand.GetCards().Count().Should().Equal(0);
        }

        [Test]
        public void can_add_a_card_to_an_empty_hand()
        {
            // Arrange
            var card = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new PlayerHand();

            // Act
            hand.AddCard(card);

            // Assert
            hand.GetCards().Should().Not.Be.Null();
            hand.GetCards().Count().Should().Equal(1);
        }

        [Test]
        public void can_add_a_second_card_to_a_hand()
        {
            // Arrange
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            IBlackjackHand hand = new PlayerHand();

            // Act
            hand.AddCard(card1);
            hand.AddCard(card2);

            // Assert
            hand.GetCards().Should().Not.Be.Null();
            hand.GetCards().Count().Should().Equal(2);
            hand.GetCards().ElementAt(0).Should().Be.SameAs(card1);
            hand.GetCards().ElementAt(1).Should().Be.SameAs(card2);
        }

        [Test]
        public void returns_value()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand();
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
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var argHand = Substitute.For<IBlackjackHand>();
            
            handValueCalculator.Value(argHand).ReturnsForAnyArgs(10, 19); 
            
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();

            IBlackjackHand hand = new PlayerHand();

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
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var argHand = Substitute.For<IBlackjackHand>();

            handValueCalculator.Value(argHand).ReturnsForAnyArgs(9, 15, 21);

            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();
            var card3 = Substitute.For<IBlackjackCard>();

            IBlackjackHand hand = new PlayerHand();

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
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand();
            handValueCalculator.Value(hand).Returns(22);

            hand.HandValueCalculator = handValueCalculator;

            // Act
            var busted = hand.Busted;

            // Assert
            busted.Should().Be.True();
        }

        [Test]
        public void hand_shows_not_busted_when_value_under_22()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();

            IBlackjackHand hand = new PlayerHand();
            handValueCalculator.Value(hand).Returns(20);

            hand.HandValueCalculator = handValueCalculator;

            // Act
            var busted = hand.Busted;

            // Assert
            busted.Should().Be.False();
        }

        [Test]
        public void hand_shows_blackjack_with_initial_hand_value_of_21()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var card1 = Substitute.For<IAceCard>();
            card1.Value.Returns(11);
            var card2 = Substitute.For<IBlackjackCard>();
            card2.Value.Returns(10);

            IPlayerHand hand = new PlayerHand();
            handValueCalculator.Value(hand).Returns(21);
            hand.HandValueCalculator = handValueCalculator;

            hand.AddCard(card1);
            hand.AddCard(card2);

            // Act
            var hasBlackJack = hand.HasBlackjack;

            // Assert
            hasBlackJack.Should().Be.True();
        }

        [Test]
        public void bet_switches_to_blackjackbet_when_hand_has_blackjack()
        {
            // Arrange
            var handValueCalculator = Substitute.For<IHandValueCalculator>();
            var card1 = Substitute.For<IAceCard>();
            card1.Value.Returns(11);
            var card2 = Substitute.For<IBlackjackCard>();
            card2.Value.Returns(10);
            var anteBet = Substitute.For<AnteBet>(10.0);

            IPlayerHand hand = new PlayerHand();
            handValueCalculator.Value(hand).Returns(21);
            hand.HandValueCalculator = handValueCalculator;

            hand.Bet = anteBet;
            hand.AddCard(card1);
            hand.AddCard(card2);
            
            // Act
            var bet = hand.Bet;

            // Assert
            bet.Should().Be.OfType<BlackjackBet>();
        }

        [Test]
        public void hand_split_result_in_two_hands_of_one_card_each()
        {
            // Arrange
            var hand = new PlayerHand();
            var card1 = Substitute.For<IBlackjackCard>();
            card1.Value.Returns(5);
            var card2 = Substitute.For<IBlackjackCard>();
            card2.Value.Returns(5);

            hand.AddCard(card1);
            hand.AddCard(card2);

            var hand2 = new PlayerHand();

            // Act
            hand.SplitInto(hand2);

            // Assert
            hand.GetCards().Count.Should().Equal(1);
            hand2.GetCards().Count.Should().Equal(1);

            hand.GetCards()[0].Should().Be.SameAs(card1);
            hand2.GetCards()[0].Should().Be.SameAs(card2);
        }
    }
}

