namespace Blackjack
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services, GameConfiguration gameConfiguration)
        {
            services
                .AddSingleton(gameConfiguration)
                .AddTransient<IHand, Hand>()
                .AddTransient<ICard, Card>()
                .AddTransient<IShoeService, ShoeService>()
                .AddTransient<ICardCountingService, CardCountingService>()
                .AddTransient<ICountingSystemFactory, CountingSystemFactory>()
                .AddTransient<IOutputService, ConsoleOutputService>()
                .AddTransient<IInputService, ConsoleInputService>()
                .AddTransient<IGameOutputService, ConsoleGameOutputService>()
                .AddTransient<IGameInputService, ConsoleGameInputService>()
                .AddTransient<IGameDialogService, ConsoleGameDialogService>()
                .AddTransient<IResultStorageService, ResultStorageService>()
                .AddTransient<IHandEvaluator, SimpleHandEvaluator>()
                .AddTransient<RoundManager>()
                .AddTransient<GameService>();
        }
    }
}
