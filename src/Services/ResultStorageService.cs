namespace Blackjack.Services
{
    public class ResultStorageService : IResultStorageService
    {
        private readonly IGameOutputService _gameOutputService;
        private readonly List<HandResult> _sessionResults;

        public ResultStorageService(IGameOutputService gameOutputService)
        {
            _gameOutputService = gameOutputService;
            _sessionResults = new List<HandResult>();
        }

        public void StoreHandResult(HandResult handResult)
        {
            _sessionResults.Add(handResult);
        }

        public void DumpRoundResults()
        {
            _gameOutputService.GameOver();

            foreach (var result in _sessionResults)
            {
                _gameOutputService.ShowResult(result);
            }
        }
    }
}
