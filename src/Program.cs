namespace Blackjack
{
    public class Program
    {
        public static void Main()
        {
            var serviceCollection = new ServiceCollection();
            var config = new GameConfiguration
            {
                DecksInShoe = 6,
                MinimumBet = 5,
                MaximumBet = 500,
                MinimumHands = 1,
                MaximumHands = 7,
                PenetrationRateRange = (0.5, 0.75),
                Strategy = Strategy.Basic,
                CountingSystem = CountingSystem.HiLo,
                InsuranceOption = false,
                DealerHitsOnSoft17 = true,
            };
            Startup.ConfigureServices(serviceCollection, config);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var gameService = serviceProvider.GetService<IGameService>();
            if (gameService == null)
            {
                Console.WriteLine("Failed to retrieve GameService from the service provider.");
                return;
            }
            gameService.PlayGame();
        }
    }
}

