namespace Blackjack.Services
{
    public interface IGameService
    {
        void PlayGame();
        void RunSimulation(SimulationConfiguration simulationConfiguration);
    }
}
