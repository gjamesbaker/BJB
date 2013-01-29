using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Should.Fluent;

namespace Blackjack.UnitTests
{
    [TestFixture]
    public class HandValueCalculatorTests
    {
        [Test, TestCaseSource("TwoCardsNoAce")]
        public void calculates_best_2_card_score_no_ace(int card1Value, int card2Value, int handValue)
        {
            // Arrange
            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();

            card1.Value.Returns(card1Value);
            card2.Value.Returns(card2Value);
            
            var cards = new List<IBlackjackCard>(){card1, card2};

            var hand = Substitute.For<IBlackjackHand>();
            hand.GetCards().Returns(cards);

            var handValueCalculator = new HandValueCalculator();

            // Act
            var value = handValueCalculator.Value(hand);

            // Assert
            value.Should().Equal(handValue);
        }

        [Test, TestCaseSource("TwoCardsWithAce")]
        public void calculates_best_2_card_score_with_an_ace(int card1Value, int card2Value, int handValue)
        {
            // Arrange
            var card1 = card1Value == 11 ? Substitute.For<IAceCard>() : Substitute.For<IBlackjackCard>();
            var card2 = card2Value == 11 ? Substitute.For<IAceCard>() : Substitute.For<IBlackjackCard>();

            card1.Value.Returns(card1Value);
            card2.Value.Returns(card2Value);

            var cards = new List<IBlackjackCard>() { card1, card2 };

            var hand = Substitute.For<IBlackjackHand>();
            hand.GetCards().Returns(cards);

            var handValueCalculator = new HandValueCalculator();

            // Act
            var value = handValueCalculator.Value(hand);

            // Assert
            value.Should().Equal(handValue);
        }

        [Test, TestCaseSource("ThreeCards")]
        public void calculates_best_3_card_score(int card1Value, int card2Value, int card3Value, int handValue)
        {
            // Arrange
            var card1 = card1Value == 11 ? Substitute.For<IAceCard>() : Substitute.For<IBlackjackCard>();
            var card2 = card2Value == 11 ? Substitute.For<IAceCard>() : Substitute.For<IBlackjackCard>();
            var card3 = card3Value == 11 ? Substitute.For<IAceCard>() : Substitute.For<IBlackjackCard>();

            card1.Value.Returns(card1Value);
            card2.Value.Returns(card2Value);
            card3.Value.Returns(card3Value);

            var cards = new List<IBlackjackCard>() { card1, card2, card3 };

            var hand = Substitute.For<IBlackjackHand>();
            hand.GetCards().Returns(cards);

            var handValueCalculator = new HandValueCalculator();

            // Act
            var value = handValueCalculator.Value(hand);

            // Assert
            value.Should().Equal(handValue);
        }

        private static readonly object[] TwoCardsNoAce =
            {
                new object[] {2, 2, 4},
                new object[] {2, 10, 12},
                new object[] {10, 3, 13},
                new object[] {10, 10, 20}
            };

        private static readonly object[] TwoCardsWithAce =
            {
                new object[] {11, 2, 13},
                new object[] {11, 10, 21},
                new object[] {9, 11, 20},
                new object[] {10, 11, 21},
                new object[] {11, 11, 12}
            };

        private static readonly object[] ThreeCards =
            {
                new object[] {2, 2, 2, 6},
                new object[] {2, 3, 4, 9},
                new object[] {2, 10, 10, 22},
                new object[] {2, 10, 11, 13},
                new object[] {11, 8, 2, 21}
            };

    }
    
}
