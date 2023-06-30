namespace Blackjack.Services
{
    public class ConsoleGameInputService : IGameInputService
    {
        private readonly IGameDialogService _gameDialogService;

        public ConsoleGameInputService(IGameDialogService gameDialogService)
        {
            _gameDialogService = gameDialogService;
        }

        public int GetNumberOfHands() => _gameDialogService.GetNumberOfHands();

        public PlayerAction GetPlayerAction(List<PlayerAction> availableActions) => _gameDialogService.GetPlayerAction(availableActions);

        public decimal GetBetAmount() => _gameDialogService.GetBetAmount();

        public bool PlayAnotherRound() => _gameDialogService.PlayAnotherRound();

        public void PlayerBust() => _gameDialogService.PlayerBust();

        public void PlayerBlackjack() => _gameDialogService.PlayerBlackjack();

        public SimulationConfiguration? GetSimulationConfiguration() => _gameDialogService.GetSimulationConfiguration();
    }
}
