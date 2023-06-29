namespace Blackjack.Models
{
    public class ConsoleGameOutputService : IGameOutputService
    {
        private readonly IOutputService _outputService;

        public ConsoleGameOutputService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void NewHand()
        {
            _outputService.WriteLine("=== New Hand ===");
        }

        public void PlayerHand(string hand)
        {
            _outputService.WriteLine($"Player's Hand: {hand}");
        }

        public void DealerHand(string hand)
        {
            _outputService.WriteLine($"Dealer's Hand: {hand}");
        }

        public void Recommendation(string recommendation)
        {
            _outputService.WriteLine("");
            _outputService.WriteLine($"Recommendation: {recommendation}");
            _outputService.WriteLine("");
        }

        public void Push()
        {
            _outputService.WriteLine("=== Push! Both player and dealer have blackjack. ===");
        }

        public void PlayerWinsWithBlackjack()
        {
            _outputService.WriteLine("+++ Player wins with blackjack! +++");
        }

        public void DealerWinsWithBlackjack()
        {
            _outputService.WriteLine("--- Dealer wins with blackjack! ---");
        }

        public void PlayerBusts()
        {
            _outputService.WriteLine("");
            _outputService.WriteLine("--- Player busts! Dealer wins. ---");
            _outputService.WriteLine("");
        }

        public void DealerBusts()
        {
            _outputService.WriteLine("");
            _outputService.WriteLine("Dealer busts! Player wins.");
            _outputService.WriteLine("");
        }

        public void PlayerWins()
        {
            _outputService.WriteLine("+++ Player wins! +++");
        }

        public void DealerWins()
        {
            _outputService.WriteLine("--- Dealer wins! ---");
        }

        public void Tie()
        {
            _outputService.WriteLine("=== Push! It's a tie. ===");
        }

        public void InvalidInput()
        {
            _outputService.WriteLine("Invalid input. Please try again.");
        }

        public PlayerAction GetPlayerAction()
        {
            _outputService.WriteLine("Choose an action:");
            _outputService.WriteLine("1. Hit");
            _outputService.WriteLine("2. Stand");
            _outputService.WriteLine("3. Double");
            _outputService.WriteLine("===============");

            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out var choice) && Enum.IsDefined(typeof(PlayerAction), choice - 1))
                {
                    _outputService.WriteLine($"Player chooses {((PlayerAction)(choice - 1)).ToString().ToLower()}.");
                    return (PlayerAction)(choice - 1);
                }

                InvalidInput();
            }
        }

        public void ReshuffleShoe()
        {
            _outputService.WriteLine("Reshuffling the shoe...");
        }
    }
}