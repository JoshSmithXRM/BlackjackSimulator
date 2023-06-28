namespace Blackjack
{
    public static class CompositionRoot
    {
        public static void Compose(int deckCount) // Add deckCount parameter
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient(provider => new Shoe(deckCount)) // Inject deckCount parameter for Shoe
                .AddTransient<IHand, Hand>()
                .AddTransient<ICard, Card>()
                .AddTransient<GameService>() // Add GameService to dependencies
                .BuildServiceProvider();

            var gameService = serviceProvider.GetService<GameService>();
            gameService.PlayGame();
        }
    }
}