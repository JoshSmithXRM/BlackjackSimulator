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

        public decimal GetWagerAmount()
        {
            return _gameDialogService.GetWagerAmount();
        }
    }
}
