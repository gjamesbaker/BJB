namespace Blackjack
{
    public class PlayerHand : Hand
    {
        public PlayerHand(IBlackjackPlayer player)
        {
            Player = player;
        }
    }
}