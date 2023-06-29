namespace Blackjack.Services
{
    public class ConsoleGameDialogService : IGameDialogService
    {
        private readonly IOutputService _outputService;
        private readonly IInputService _inputService;

        public ConsoleGameDialogService(IOutputService outputService, IInputService inputService)
        {
            _outputService = outputService;
            _inputService = inputService;
        }

        public PlayerAction GetPlayerAction(List<PlayerAction> availableActions)
        {
            while (true)
            {
                _outputService.WriteLine("Choose an action:");
                for (int i = 0; i < availableActions.Count; i++)
                {
                    _outputService.WriteLine($"{i + 1}. {availableActions[i]}");
                }

                int choice = _inputService.ReadIntegerInput();

                if (choice >= 1 && choice <= availableActions.Count)
                {
                    return availableActions[choice - 1];
                }

                _outputService.WriteLine("Invalid input. Please try again.");
            }
        }

        public int GetNumberOfHands()
        {
            while (true)
            {
                _outputService.WriteLine("Enter the number of hands to play:");
                int numberOfHands = _inputService.ReadIntegerInput();

                if (numberOfHands > 0)
                {
                    return numberOfHands;
                }

                _outputService.WriteLine("Invalid input. Please try again.");
            }
        }

        public decimal GetBetAmount()
        {
            while (true)
            {
                _outputService.WriteLine("Enter the wager amount for each hand:");
                decimal wagerAmount = _inputService.ReadDecimalInput();

                if (wagerAmount > 0)
                {
                    return wagerAmount;
                }

                _outputService.WriteLine("Invalid input. Please try again.");
            }
        }

        public bool PlayAnotherRound()
        {
            while (true)
            {
                _outputService.WriteLine();
                _outputService.WriteLine("Play another round? (Y/N)");
                string input = _inputService.ReadInput();

                if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                _outputService.WriteLine("Invalid input. Please try again.");
            }
        }

        public void PlayerBust()
        {
            _outputService.WriteLine("Bust! Press any key to continue.");
            _inputService.ReadInput();
        }

        public void PlayerBlackjack()
        {
            _outputService.WriteLine("Blackjack! Press any key to continue.");
            _inputService.ReadInput();
        }
    }
}