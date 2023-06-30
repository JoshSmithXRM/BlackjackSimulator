namespace Blackjack.Services
{
    public class ConsoleGameDialogService : IGameDialogService
    {
        private readonly IOutputService _outputService;
        private readonly IInputService _inputService;
        private readonly GameConfiguration _gameConfiguration;

        public ConsoleGameDialogService(IOutputService outputService, IInputService inputService, GameConfiguration gameConfiguration)
        {
            _outputService = outputService;
            _inputService = inputService;
            _gameConfiguration = gameConfiguration;
        }

        public PlayerAction GetPlayerAction(List<PlayerAction> availableActions)
        {
            while (true)
            {
                _outputService.WriteLine("Choose an action:");
                for (var i = 0; i < availableActions.Count; i++)
                {
                    _outputService.WriteLine($"{i + 1}. {availableActions[i]}");
                }

                var choice = _inputService.ReadIntegerInput();

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
                _outputService.WriteLine($"Enter the number of hands to play (Min: {_gameConfiguration.MinimumHands} Max: {_gameConfiguration.MaximumHands}):");
                var numberOfHands = _inputService.ReadIntegerInput();

                if (numberOfHands <= _gameConfiguration.MaximumHands && numberOfHands >= _gameConfiguration.MinimumHands)
                {
                    return numberOfHands;
                }
                else
                {
                    _outputService.WriteLine($"Invalid number of hands. Please enter a value between {_gameConfiguration.MinimumHands} and {_gameConfiguration.MaximumHands}.");
                }
            }
        }

        public decimal GetBetAmount()
        {
            while (true)
            {
                _outputService.WriteLine($"Enter the bet amount for each hand (Min: {_gameConfiguration.MinimumBet} Max: {_gameConfiguration.MaximumBet}):");
                var betAmount = _inputService.ReadDecimalInput();

                if (betAmount <= _gameConfiguration.MaximumBet && betAmount >= _gameConfiguration.MinimumBet)
                {
                    return betAmount;
                }
                else
                {
                    _outputService.WriteLine($"Invalid bet amount. Please enter a value between {_gameConfiguration.MinimumBet} and {_gameConfiguration.MaximumBet}.");
                }
            }
        }

        public bool PlayAnotherRound()
        {
            while (true)
            {
                _outputService.WriteLine();
                _outputService.WriteLine("Play another round? (Y/N)");
                var input = _inputService.ReadInput();

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
            _ = _inputService.ReadInput();
        }

        public void PlayerBlackjack()
        {
            _outputService.WriteLine("Blackjack! Press any key to continue.");
            _ = _inputService.ReadInput();
        }

        public SimulationConfiguration? GetSimulationConfiguration()
        {
            _outputService.WriteLine("Do you want to run in simulation mode? (Y/N)");
            var input = _inputService.ReadInput();
            return input.Equals("Y", StringComparison.OrdinalIgnoreCase)
                ? new SimulationConfiguration()
                {
                    NumberOfRounds = GetNumberOfRounds(),
                    NumberOfHands = GetNumberOfHands(),
                    BetAmount = GetBetAmount()
                }
                : null;
        }

        private int GetNumberOfRounds()
        {
            while (true)
            {
                _outputService.WriteLine($"Enter the number of rounds to play (Min: {_gameConfiguration.MinimumRounds} Max: {_gameConfiguration.MaximumRounds}):");
                var numberOfRounds = _inputService.ReadIntegerInput();

                if (numberOfRounds <= _gameConfiguration.MaximumRounds && numberOfRounds >= _gameConfiguration.MinimumRounds)
                {
                    return numberOfRounds;
                }
                else
                {
                    _outputService.WriteLine($"Invalid number of hands. Please enter a value between {_gameConfiguration.MinimumRounds} and {_gameConfiguration.MaximumRounds}.");
                }
            }
        }
    }
}