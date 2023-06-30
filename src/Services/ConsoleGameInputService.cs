namespace Blackjack.Services
{
    public class ConsoleGameInputService : IGameInputService
    {
        private readonly IGameDialogService _gameDialogService;

        public ConsoleGameInputService(IGameDialogService gameDialogService)
        {
            _gameDialogService = gameDialogService;
        }

        public int GetNumberOfHands()
        {
            return _gameDialogService.GetNumberOfHands();
        }

        public PlayerAction GetPlayerAction(List<PlayerAction> availableActions)
        {
            return _gameDialogService.GetPlayerAction(availableActions);
        }

        public decimal GetBetAmount()
        {
            return _gameDialogService.GetBetAmount();
        }

        public bool PlayAnotherRound()
        {
            return _gameDialogService.PlayAnotherRound();
        }

        public void PlayerBust()
        {
            _gameDialogService.PlayerBust();
        }

        public void PlayerBlackjack()
        {
            _gameDialogService.PlayerBlackjack();
        }

        public SimulationConfiguration? GetSimulationConfiguration()
        {
            return _gameDialogService.GetSimulationConfiguration();
        }
    }
}
