namespace Blackjack.Services
{
    public class GameService : IGameService
    {
        private readonly IShoeService _shoeService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IGameInputService _gameInputService;
        private readonly IGameOutputService _gameOutputService;
        private readonly IGameRound _gameRound;
        private readonly List<HandResult> _sessionResults;

        public GameService(
            IShoeService shoeService,
            ICardCountingService cardCountingService,
            IGameInputService gameInputService,
            IGameOutputService gameOutputService,
            IGameRound gameRound)
        {
            _shoeService = shoeService;
            _cardCountingService = cardCountingService;
            _gameInputService = gameInputService;
            _gameOutputService = gameOutputService;
            _gameRound = gameRound;
            _sessionResults = new List<HandResult>();
        }

        public void PlayGame()
        {
            _gameOutputService.ClearOutput();

            while (true)
            {
                int numberOfPlayerHands = _gameInputService.GetNumberOfHands();
                decimal betAmount = _gameInputService.GetBetAmount();

                List<HandResult> roundResults = _gameRound.Play(numberOfPlayerHands, betAmount);
                _sessionResults.AddRange(roundResults);
                ShowRoundResults(roundResults);

                if (_shoeService.NeedsReshuffling())
                {
                    ReshuffleShoe();
                }

                if (!_gameInputService.PlayAnotherRound())
                {
                    break;
                }
            }

            ShowSessionResults();
            _gameOutputService.GameOver();
        }

        private void ReshuffleShoe()
        {
            _gameOutputService.ReshuffleShoe();
            _shoeService.ShuffleCards();
            _cardCountingService.ResetCount();
        }

        private void ShowRoundResults(List<HandResult> roundResults)
        {
            _gameOutputService.ClearOutput();
            _gameOutputService.ShowResults(roundResults);
        }

        private void ShowSessionResults()
        {
            _gameOutputService.ClearOutput();
            _gameOutputService.ShowResults(_sessionResults);
        }
    }
}
