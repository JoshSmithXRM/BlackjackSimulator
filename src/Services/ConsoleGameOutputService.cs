namespace Blackjack.Services
{
    public class ConsoleGameOutputService : IGameOutputService
    {
        private readonly IOutputService _outputService;

        public ConsoleGameOutputService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void ClearOutput()
        {
            _outputService.Clear();
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
            _outputService.WriteLine();
            _outputService.WriteLine($"Recommendation: {recommendation}");
            _outputService.WriteLine();
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
            _outputService.WriteLine();
            _outputService.WriteLine("--- Player busts! Dealer wins. ---");
            _outputService.WriteLine();
        }

        public void DealerBusts()
        {
            _outputService.WriteLine();
            _outputService.WriteLine("Dealer busts! Player wins.");
            _outputService.WriteLine();
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

        public void InvalidAction()
        {
            _outputService.WriteLine("Invalid input. Please try again.");
        }

        public void ReshuffleShoe()
        {
            _outputService.WriteLine("Reshuffling the shoe...");
        }

        public void GameOver()
        {
            _outputService.WriteLine("=== Game Over ===");
        }

        public void Recommendation(Recommendation recommendation)
        {
            _outputService.WriteLine();
            _outputService.WriteLine($"Recommendation (Count: {recommendation.RunningCount} | {recommendation.CountType}): {recommendation.Action}");
            _outputService.WriteLine();
        }

        public void ShowResult(HandResult result)
        {
            _outputService.WriteLine("=== Hand Result ===");
            _outputService.WriteLine($"Outcome: {result.Outcome}");
            _outputService.WriteLine($"Player's Hand: {result.PlayerCards}");
            _outputService.WriteLine($"Dealer's Hand: {result.DealerCards}");
            _outputService.WriteLine($"Bet Amount: {result.BetAmount}");
            _outputService.WriteLine($"Net Amount Won/Lost: {result.NetAmountWonLost}");
            _outputService.WriteLine($"Total Amount: {result.TotalAmount}");
            _outputService.WriteLine($"Running Count: {result.RunningCount}");
            _outputService.WriteLine($"Count Type: {result.CountType}");
            _outputService.WriteLine("==================");
        }

        public void ShowHands(Hand playerHand, Hand dealerHand)
        {
            _outputService.WriteLine("Player Hand:");
            foreach (var card in playerHand.Cards)
            {
                Console.WriteLine(card.ToString());
            }
            _outputService.WriteLine("Total: " + playerHand.GetTotal());

            _outputService.WriteLine();

            _outputService.WriteLine("Dealer Hand:");
            foreach (var card in dealerHand.Cards)
            {
                _outputService.WriteLine(card.ToString() ?? string.Empty);
            }
            _outputService.WriteLine("Total: " + dealerHand.GetTotal());

            _outputService.WriteLine();
        }

        public void DisplayResult(HandOutcome outcome)
        {
            switch (outcome)
            {
                case HandOutcome.Win:
                    _outputService.WriteLine("You win!");
                    break;
                case HandOutcome.Loss:
                    _outputService.WriteLine("You lose!");
                    break;
                case HandOutcome.Push:
                    _outputService.WriteLine("It's a tie!");
                    break;
                default:
                    _outputService.WriteLine("Unknown outcome.");
                    break;
            }
        }
    }
}