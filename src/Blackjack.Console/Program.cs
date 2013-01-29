namespace Blackjack.Console
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IBlackjackTable table = new BlackjackTable();
            IBlackjackPlayer player1 = new BlackjackPlayer {Balance = 2000, Ante = 20};
            IBlackjackPlayer player2 = new BlackjackPlayer {Balance = 3000, Ante = 30};
            table.AddPlayer(player1);
            table.AddPlayer(player2);

            table.ShuffleShoe();

            PerformOneGame(table);
        }

        private static void PerformOneGame(IBlackjackTable table)
        {
            var game = new Game(table);

            System.Console.WriteLine();
            System.Console.WriteLine("***************************");
            System.Console.WriteLine("******   NEW ROUND   ******");
            System.Console.WriteLine("***************************");
            System.Console.WriteLine();

            game.StartNewGame();
            game.CallForBets();
            game.DealHands();

            System.Console.WriteLine("***   Initial Deal   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());

            game.OfferSplits();

            System.Console.WriteLine("***   After OfferSplits   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());

            game.OfferDoubleDowns();

            System.Console.WriteLine("***   After OfferDoubleDowns   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());

            game.FillPlayerHands();

            System.Console.WriteLine("***   After FillPlayerHands   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());

            game.FillDealerHand();

            System.Console.WriteLine("***   After FillDealerHands   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());

            var amount = game.SettleBets();
            var winnings = string.Format("House Winnings: {0:C}", amount);

            System.Console.WriteLine("***   After SettleBets   ***");
            System.Console.WriteLine();
            System.Console.WriteLine(game.ToString());
            System.Console.WriteLine(winnings);

            System.Console.ReadLine();
        }
    }
}
