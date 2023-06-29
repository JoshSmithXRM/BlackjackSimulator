namespace Blackjack.Services
{
    public class GameService
    {
        private readonly IShoeService _shoeService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IGameInputService _gameInputService;
        private readonly IGameOutputService _gameOutputService;
        private readonly IResultStorageService _resultStorageService;
        private readonly GameConfiguration _gameConfiguration;
        private readonly RoundManager _roundManager;
        private readonly IHandEvaluator _handEvaluator;

        public GameService(
            IShoeService shoeService,
            ICardCountingService cardCountingService,
            IGameInputService gameInputService,
            IGameOutputService gameOutputService,
            IResultStorageService resultStorageService,
            GameConfiguration gameConfiguration,
            RoundManager roundManager,
            IHandEvaluator handEvaluator)
        {
            _shoeService = shoeService;
            _cardCountingService = cardCountingService;
            _gameInputService = gameInputService;
            _gameOutputService = gameOutputService;
            _resultStorageService = resultStorageService;
            _gameConfiguration = gameConfiguration;
            _roundManager = roundManager;
            _handEvaluator = handEvaluator;
        }

        public void PlayGame()
        {
            _gameOutputService.ClearOutput();

            int numberOfPlayerHands = _gameInputService.GetNumberOfHands();
            decimal wagerAmount = _gameInputService.GetWagerAmount();

            

            for (int i = 0; i < numberOfPlayerHands; i++)
            {
                PlayHand(wagerAmount);
                _gameOutputService.ClearOutput();
            }

            _roundManager.CompleteRound();
           
        }

        private void PlayHand(decimal wagerAmount)
        {
            Hand playerHand = new(wagerAmount);
            Hand dealerHand = new();

            playerHand.AddCard(_shoeService.DrawCard());
            dealerHand.AddCard(_shoeService.DrawCard());
            playerHand.AddCard(_shoeService.DrawCard());
            dealerHand.AddCard(_shoeService.DrawCard());

            var playerActionsTaken = new List<PlayerAction>();

            while (true)
            {
                _gameOutputService.ShowHands(playerHand, dealerHand);

                Recommendation recommendation = _cardCountingService.GetRecommendation(playerHand, dealerHand.GetUpcard());
                _gameOutputService.Recommendation(recommendation);

                var validActions = new List<PlayerAction>
                {
                    PlayerAction.Hit,
                    PlayerAction.Stand
                };

                if (playerHand.CanDoubleDown)
                {
                    validActions.Add(PlayerAction.Double);
                }

                if (playerHand.CanSplit)
                {
                    validActions.Add(PlayerAction.Split);
                }

                PlayerAction playerAction = _gameInputService.GetPlayerAction(validActions);
                playerActionsTaken.Add(playerAction);
                switch (playerAction)
                {
                    case PlayerAction.Hit:
                        playerHand.AddCard(_shoeService.DrawCard());
                        if (playerHand.IsBust)
                        {
                            _gameOutputService.PlayerBusts();
                            _resultStorageService.StoreHandResult(_handEvaluator.EvaluateHand(playerHand, dealerHand, _cardCountingService.RunningCount, _cardCountingService.CountType, playerActionsTaken));
                            return;
                        }
                        break;
                    case PlayerAction.Stand:
                        break;
                    case PlayerAction.Double:
                        if (playerHand.CanDoubleDown)
                        {
                            playerHand.AddCard(_shoeService.DrawCard());
                            if (playerHand.IsBust)
                            {
                                _gameOutputService.PlayerBusts();
                                _resultStorageService.StoreHandResult(_handEvaluator.EvaluateHand(playerHand, dealerHand, _cardCountingService.RunningCount, _cardCountingService.CountType, playerActionsTaken));
                            }
                            break;
                        }
                        else
                        {
                            _gameOutputService.InvalidAction();
                            continue;
                        }
                    default:
                        _gameOutputService.InvalidAction();
                        continue;
                }

                if (playerAction == PlayerAction.Stand || playerHand.IsBust)
                    break;
            }

            while (dealerHand.GetTotal() < 17 && !playerHand.IsBust)
            {
                dealerHand.AddCard(_shoeService.DrawCard());
                _gameOutputService.ShowHands(playerHand, dealerHand);
            }

            _gameOutputService.ShowHands(playerHand, dealerHand);

            HandResult handResult = _handEvaluator.EvaluateHand(playerHand, dealerHand, _cardCountingService.RunningCount, _cardCountingService.CountType, playerActionsTaken);

            _gameOutputService.DisplayResult(handResult.Outcome);
            _resultStorageService.StoreHandResult(handResult);
        }
    }
}
