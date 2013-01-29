namespace Blackjack.Cards
{
    public class FaceCard : Card
    {
        protected internal FaceCard(Rank rank, Suit suit) : base(rank, suit)
        {}

        public override int Value
        {
            get { return 10; }
        }
    }
}