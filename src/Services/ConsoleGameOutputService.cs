using System.Security.Cryptography.X509Certificates;

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

        public void Recommendation(string recommendation)
        {
            _outputService.WriteLine($"Recommendation: {recommendation}");
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
            _outputService.WriteLine($"Recommendation (Count: {recommendation.RunningCount} | {recommendation.CountType}): {recommendation.Action}");
            _outputService.WriteLine();
        }

        public void ShowResults(List<HandResult> results)
        {
            _outputService.Clear();
            _outputService.WriteLine("=== Hand Results ===");

            foreach (var result in results)
            {
                _outputService.WriteLine($"Hand # {results.IndexOf(result) + 1}/{results.Count}");
                _outputService.WriteLine($"Outcome: {result.Outcome}");
                _outputService.WriteLine($"Player's Hand: {result.PlayerHand.PartialString()} ({result.PlayerHand.GetTotal()})");
                _outputService.WriteLine($"Dealer's Hand: {result.DealerHand.PartialString()} ({result.DealerHand.GetTotal()})");
                _outputService.WriteLine($"Bet Amount: {result.BetAmount}");
                _outputService.WriteLine($"Net Amount Won/Lost: {result.NetAmountWonLost}");
                _outputService.WriteLine($"Player Actions: {string.Join(", ", result.PlayerActions)}");
                _outputService.WriteLine();
            }

            _outputService.WriteLine($"Total Amount Won/Lost: {results.Sum(r => r.NetAmountWonLost)}");
            _outputService.WriteLine($"Blackjacks: {results.Count(x => x.Outcome == HandOutcome.Blackjack)}");
            _outputService.WriteLine($"Wins: {results.Count(x => x.Outcome == HandOutcome.Win)} ");
            _outputService.WriteLine($"Losses: {results.Count(x => x.Outcome == HandOutcome.Loss)}");
            _outputService.WriteLine($"Pushes: {results.Count(x => x.Outcome == HandOutcome.Push)}");
            _outputService.WriteLine($"Surrenders: {results.Count(x => x.Outcome == HandOutcome.Surrender)}");
            _outputService.WriteLine("==================");
        }

        public void ShowHands(IHand playerHand, IHand dealerHand, bool hideSecondCard = false)
        {
            _outputService.WriteLine("Player Hand:");
            _outputService.WriteLine(playerHand.PartialString());
            _outputService.WriteLine("Total: " + playerHand.GetTotal());

            _outputService.WriteLine();

            _outputService.WriteLine("Dealer Hand:");
            _outputService.WriteLine(dealerHand.PartialString(hideSecondCard));
            if (hideSecondCard)
            {
                _outputService.WriteLine("Total: " + dealerHand.GetUpCard().GetRankValue());
            }
            else
            {
                _outputService.WriteLine("Total: " + dealerHand.GetTotal());
            }
            _outputService.WriteLine();
        }
    }
}