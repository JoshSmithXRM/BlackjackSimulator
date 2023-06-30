namespace Blackjack.Services
{
    public interface IGameDialogService
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