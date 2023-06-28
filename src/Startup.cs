namespace Blackjack
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services, int deckCount)
        {
            services
                .AddTransient<IShoe>(provider => new Shoe(deckCount)) 
                .AddTransient<IHand, Hand>()
                .AddTransient<ICard, Card>()
                .AddTransient<GameService>()
                .AddTransient<ICardCountingService, CardCountingService>();
        }
    }
}