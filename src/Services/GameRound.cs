namespace Blackjack.Services
{
    public class GameRound : IGameRound
    {
        private readonly IHandFactory _handFactory;
        private readonly IShoeService _shoeService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IGameInputService _gameInputService;
        private readonly IGameOutputService _gameOutputService;
        private readonly IHandEvaluator _handEvaluator;
        private readonly List<IHand> _playerHands;
        private IHand _dealerHand;
        private readonly List<HandResult> _roundResults;

        public GameRound(
            IHandFactory handFactory,
            IShoeService shoeService,
            ICardCountingService cardCountingService,
            IGameInputService gameInputService,
            IGameOutputService gameOutputService,
            IHandEvaluator handEvaluator)
        {
            _handFactory = handFactory;
            _shoeService = shoeService;
            _cardCountingService = cardCountingService;
            _gameInputService = gameInputService;
            _gameOutputService = gameOutputService;
            _handEvaluator = handEvaluator;
            _playerHands = new List<IHand>();
            _dealerHand = _handFactory.CreateHand();
            _roundResults = new List<HandResult>();
        }

        public List<HandResult> Play(int numberOfPlayerHands, decimal betAmount)
        {
            InitializeHands(numberOfPlayerHands, betAmount);
            DealInitialCards();
            _roundResults.Clear();

            foreach (IHand playerHand in _playerHands)
            {
                _gameOutputService.ClearOutput();
                PlayHand(playerHand, _dealerHand);
            }

            PlayDealerHand(_dealerHand);

            foreach (var hand in _playerHands)
            {
                HandResult result = _handEvaluator.EvaluateHand(hand, _dealerHand);
                _roundResults.Add(result);
            }

            return _roundResults;
        }

        private void InitializeHands(int numberOfPlayerHands, decimal betAmount)
        {
            _dealerHand = _handFactory.CreateHand();
            _playerHands.Clear();
            for (int i = 0; i < numberOfPlayerHands; i++)
            {
                _playerHands.Add(_handFactory.CreateHand(betAmount));
            }
        }

        private void DealInitialCards()
        {
            _playerHands.ForEach(playerHand =>
            {
                playerHand.AddCard(DrawCard());
            });

            _dealerHand.AddCard(DrawCard());

            _playerHands.ForEach(playerHand =>
            {
                playerHand.AddCard(DrawCard());
            });

            _dealerHand.AddCard(_shoeService.DrawCard());
        }

        private ICard DrawCard()
        {
            var newCard = _shoeService.DrawCard();
            _cardCountingService.UpdateCount(newCard);
            return newCard;
        }

        private void PlayHand(IHand playerHand, IHand dealerHand)
        {
            while (true)
            {
                _gameOutputService.ClearOutput();
                _gameOutputService.ShowHands(playerHand, dealerHand, true);

                if (playerHand.IsBust)
                {
                    _gameInputService.PlayerBust();
                    return;
                }

                if (playerHand.IsBlackjack || playerHand.GetTotal() == 21)
                {
                    _gameInputService.PlayerBlackjack();
                    return;
                }

                Recommendation recommendation = _cardCountingService.GetRecommendation(playerHand, dealerHand.GetUpCard());
                _gameOutputService.Recommendation(recommendation);

                var validActions = GetValidActions(playerHand);
                PlayerAction playerAction = _gameInputService.GetPlayerAction(validActions);
                playerHand.ActionsTaken.Add(playerAction);

                switch (playerAction)
                {
                    case PlayerAction.Hit:
                        playerHand.AddCard(DrawCard());
                        break;
                    case PlayerAction.Stand:
                        return;
                    case PlayerAction.Double:
                        playerHand.BetAmount *= 2;
                        playerHand.AddCard(DrawCard());
                        return;
                    case PlayerAction.Split:
                        var newHand = playerHand.Split();
                        playerHand.AddCard(DrawCard());
                        newHand.AddCard(DrawCard());
                        _playerHands.Add(newHand);
                        PlayHand(newHand, dealerHand);
                        return;
                    case PlayerAction.Surrender:
                        return;
                    default:
                        _gameOutputService.InvalidAction();
                        continue;
                }
            }
        }

        private static List<PlayerAction> GetValidActions(IHand playerHand)
        {
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

            if (playerHand.CanSurrender)
            {
                validActions.Add(PlayerAction.Surrender);
            }

            return validActions;
        }

        private void PlayDealerHand(IHand dealerHand)
        {
            _cardCountingService.UpdateCount(dealerHand.GetDownCard());
            while (dealerHand.GetTotal() < 17 && !dealerHand.IsBust)
            {
                dealerHand.AddCard(DrawCard());
            }
        }
    }
}