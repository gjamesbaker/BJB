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

            table.Shoe.Shuffle();

            var game = new Game(table);

            game.StartNewGame();
            game.CallForBets();
            game.DealHands();

            System.Console.WriteLine("Initial Deal:");
            var output = game.GetHandInfo();
            System.Console.WriteLine(output);

            game.OfferSplits();

            System.Console.WriteLine("After OfferSplits:");
            output = game.GetHandInfo();
            System.Console.WriteLine(output);

            game.OfferDoubleDowns();

            System.Console.WriteLine("After OfferDoubleDowns:");
            output = game.GetHandInfo();
            System.Console.WriteLine(output);

            game.FillPlayerHands();

            System.Console.WriteLine("After FillPlayerHands:");
            output = game.GetHandInfo();
            System.Console.WriteLine(output);

            game.FillDealerHand();

            System.Console.WriteLine("After FillDealerHands:");
            output = game.GetHandInfo();
            System.Console.WriteLine(output);

            //for (var i = 0; i < 20; i++)
            //{
            //    var card = table.Shoe.Deal();
            //    System.Console.WriteLine(card.ToLongString());
            //}

            System.Console.ReadLine();
        }
    }
}
