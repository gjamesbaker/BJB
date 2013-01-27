﻿using System.Collections.Generic;
using System.Linq;
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

            card1.SoftValue.Returns(card1Value);
            card1.HardValue.Returns(card1Value);
            card2.SoftValue.Returns(card2Value);
            card2.HardValue.Returns(card2Value);
            
            var cards = new List<IBlackjackCard>(){card1, card2};

            var hand = Substitute.For<IBlackjackHand>();
            hand.Cards.Returns(cards);

            var handValueCalculator = new HandValueCalculator();

            // Act
            var value = handValueCalculator.Value(hand);

            // Assert
            value.Should().Equal(handValue);
        }

        [Test, TestCaseSource("TwoCardsOneAce")]
        public void calculates_best_2_card_score_with_one_ace(int card1Value, int card2Value, int handValue)
        {
            // Arrange

            var card1 = Substitute.For<IBlackjackCard>();
            var card2 = Substitute.For<IBlackjackCard>();

            card1.SoftValue.Returns(card1Value == 1 ? 11 : card1Value);
            card1.HardValue.Returns(card1Value);
            card2.SoftValue.Returns(card2Value == 1 ? 11 : card2Value);
            card2.HardValue.Returns(card2Value);

            var cards = new List<IBlackjackCard>() { card1, card2 };

            var hand = Substitute.For<IBlackjackHand>();
            hand.Cards.Returns(cards);

            var handValueCalculator = new HandValueCalculator();

            // Act
            var value = handValueCalculator.Value(hand);

            // Assert
            value.Should().Equal(handValue);
        }

        public void calculates_best_3_card_score_with_one_ace()
        {

        }

        public void calculates_best_score_with_two_aces()
        {

        }

        public void calculates_best_score_with_two_aces_plus_one_more_card()
        {

        }

        private static readonly object[] TwoCardsNoAce =
            {
                new object[] {2, 2, 4},
                new object[] {2, 10, 12},
                new object[] {10, 3, 13},
                new object[] {10, 10, 20}
            };

        private static readonly object[] TwoCardsOneAce =
            {
                new object[] {1, 2, 13},
                new object[] {1, 10, 21},
                new object[] {9, 1, 20},
                new object[] {10, 1, 21}
            };

    }
    
}
