namespace Blackjack.CountingSystems
{
    public interface ICountingSystemFactory
    {
        ICountingSystem CreateCountingSystem(CountingSystem countingSystem);
    }
}