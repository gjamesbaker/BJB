namespace Blackjack
{
    public class AceCard : Card
    {
        protected internal AceCard(Suit suit) : base(Rank.Ace, suit)
        {}

        public override int SoftValue
        {
            get { return 11; }
        }

        public override int HardValue
        {
            get { return 1; }
        }
    }
}