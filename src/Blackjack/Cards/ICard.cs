namespace Blackjack.Cards
{
    public interface ICard
    {
        Rank Rank { get; }
        Suit Suit { get; }
        string ToString();
    }
}