namespace Blackjack.Services
{
    public class RoundManager
    {
        private readonly IShoeService _shoeService;
        private readonly IGameOutputService _gameOutputService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IResultStorageService _resultStorageService;

        public RoundManager(IShoeService shoeService, IGameOutputService gameOutputService, ICardCountingService cardCountingService, IResultStorageService resultStorageService)
        {
            _shoeService = shoeService;
            _gameOutputService = gameOutputService;
            _cardCountingService = cardCountingService;
            _resultStorageService = resultStorageService;
        }

        public void CompleteRound()
        {
            if (_shoeService.NeedsReshuffling())
            {
                ReshuffleShoe();
            }
            _gameOutputService.GameOver();
            _resultStorageService.DumpRoundResults();
        }

        private void ReshuffleShoe()
        {
            _gameOutputService.ReshuffleShoe();
            _shoeService.ShuffleCards();
            _cardCountingService.ResetCount();
        }
    }
}