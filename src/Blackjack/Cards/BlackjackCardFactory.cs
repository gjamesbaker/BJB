namespace Blackjack.Cards
{
    public static class BlackjackCardFactory
    {
        public static IBlackjackCard Get(Rank rank, Suit suit)
        {
            switch (rank)
            {
                case Rank.Ace:
                    return new AceCard(suit);
                case Rank.King:
                case Rank.Queen:
                case Rank.Jack:
                    return new FaceCard(rank, suit);
                default:
                    return new Card(rank, suit);
            }
        }
    }
}
