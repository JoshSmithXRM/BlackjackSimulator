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
                .AddTransient<IOutputService, ConsoleOutputService>()
                .AddTransient<IGameOutputService, ConsoleGameOutputService>()
                .AddTransient<GameService>();
        }
    }
}