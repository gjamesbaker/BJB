namespace Blackjack
{
    public interface ICard
    {
        Rank Rank { get; }
        Suit Suit { get; }
        string ToLongString();
    }
}