namespace Blackjack
{
    public class FaceCard : Card
    {
        protected internal FaceCard(Rank rank, Suit suit) : base(rank, suit)
        {}

        public override int SoftValue
        {
            get { return 10; }
        }

        public override int HardValue
        {
            get { return 10; }
        }
    }
}