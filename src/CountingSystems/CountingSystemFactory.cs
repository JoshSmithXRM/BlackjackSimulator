namespace Blackjack.CountingSystems
{
    public class CountingSystemFactory : ICountingSystemFactory
    {
        public ICountingSystem CreateCountingSystem(CountingSystem countingSystem)
        {
            return countingSystem switch
            {
                CountingSystem.HiLo => new HiloCountingSystem(),
                // Add cases for other counting systems here
                _ => throw new NotImplementedException($"Counting system '{countingSystem}' is not supported.")
            };
        }
    }
}
