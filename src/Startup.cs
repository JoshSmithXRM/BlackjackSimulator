namespace Blackjack
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services, GameConfiguration gameConfiguration)
        {
            services
                .AddSingleton(gameConfiguration)
                .AddTransient<IShoeService, ShoeService>()
                .AddTransient<ICardCountingService, CardCountingService>()
                .AddTransient<ICountingSystemFactory, CountingSystemFactory>()
                .AddTransient<IOutputService, ConsoleOutputService>()
                .AddTransient<IInputService, ConsoleInputService>()
                .AddTransient<IGameOutputService, ConsoleGameOutputService>()
                .AddTransient<IGameInputService, ConsoleGameInputService>()
                .AddTransient<IGameDialogService, ConsoleGameDialogService>()
                .AddTransient<IHandEvaluator, HandEvaluator>()
                .AddTransient<IHandFactory, HandFactory>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IGameRound, GameRound>();
        }
    }
}
