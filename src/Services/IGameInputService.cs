namespace Blackjack.Services
{
    public interface IGameInputService
    {
        PlayerAction GetPlayerAction(List<PlayerAction> availableActions);
        int GetNumberOfHands();
        decimal GetBetAmount();
        bool PlayAnotherRound();
        void PlayerBust();
        void PlayerBlackjack();
        SimulationConfiguration? GetSimulationConfiguration();
    }
}