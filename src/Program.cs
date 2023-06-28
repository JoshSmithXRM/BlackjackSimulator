namespace Blackjack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int deckCount = 8;

            var serviceCollection = new ServiceCollection();
            var startup = new Startup();
            startup.ConfigureServices(serviceCollection, deckCount);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var gameService = serviceProvider.GetService<GameService>();
            if (gameService == null)
            {
                Console.WriteLine("Failed to retrieve GameService from the service provider.");
                return;
            }
            gameService.PlayGame();
        }
    }
}

