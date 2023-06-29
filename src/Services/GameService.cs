/*
Reafactoring Remaining:
Extract the creation of Hand objects into an IHandFactory interface and a corresponding implementation (HandFactory). This adheres to the Dependency Inversion Principle (DIP).

Move the evaluation of hands (EvaluateHand) outside of the PlayHand method and into a separate method (EvaluateHand) to improve readability and separation of concerns.

Rename the _finalHands field to a more descriptive name, such as _activeHands, to accurately reflect its purpose.

Consider extracting the game-related logic into a separate class, such as Game or GameRound, to encapsulate the game-specific behavior and reduce the responsibilities of the GameService.

Evaluate whether the GameService class has grown too large and if there's an opportunity to further split it into smaller, more focused classes.

Review the error handling and exception handling in the code to ensure robustness and proper handling of unexpected scenarios.

Consider applying SOLID design principles (Single Responsibility Principle, Open/Closed Principle, Liskov Substitution Principle, Interface Segregation Principle, and Dependency Inversion Principle) to ensure better maintainability, testability, and extensibility of the codebase.
*/
namespace Blackjack.Services
{
    public class GameService
    {
        private readonly IShoeService _shoeService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IGameInputService _gameInputService;
        private readonly IGameOutputService _gameOutputService;
        private readonly GameConfiguration _gameConfiguration;
        private readonly IHandEvaluator _handEvaluator;
        private readonly List<IHand> _playerHands;
        private readonly List<IHand> _finalHands;
        private IHand _dealerHand;
        private readonly List<HandResult> _roundResults;
        private readonly List<HandResult> _sessionResults;

        public GameService(
            IShoeService shoeService,
            ICardCountingService cardCountingService,
            IGameInputService gameInputService,
            IGameOutputService gameOutputService,
            GameConfiguration gameConfiguration,
            IHandEvaluator handEvaluator)
        {
            _shoeService = shoeService;
            _cardCountingService = cardCountingService;
            _gameInputService = gameInputService;
            _gameOutputService = gameOutputService;
            _gameConfiguration = gameConfiguration;
            _handEvaluator = handEvaluator;
            _playerHands = new List<IHand>();
            _finalHands = new List<IHand>();
            _dealerHand = new Hand();
            _roundResults = new List<HandResult>();
            _sessionResults = new List<HandResult>();
        }

        public void PlayGame()
        {
            _gameOutputService.ClearOutput();

            while (true)
            {
                int numberOfPlayerHands = _gameInputService.GetNumberOfHands();
                decimal betAmount = _gameInputService.GetBetAmount();

                InitializeHands(numberOfPlayerHands, betAmount);
                DealInitialCards();
                _roundResults.Clear();

                foreach (IHand playerHand in _playerHands)
                {
                    _gameOutputService.ClearOutput();
                    _finalHands.Add(playerHand);
                    PlayHand(playerHand, _dealerHand);
                }

                PlayDealerHand(_dealerHand);

                foreach (var hand in _finalHands)
                {
                    HandResult result = _handEvaluator.EvaluateHand(hand, _dealerHand);
                    _roundResults.Add(result);
                    _sessionResults.Add(result);
                }

                _gameOutputService.ClearOutput();
                _gameOutputService.ShowResults(_roundResults);

                if (_shoeService.NeedsReshuffling())
                {
                    ReshuffleShoe();
                }

                if (!_gameInputService.PlayAnotherRound())
                {
                    break;
                }
            }

            _gameOutputService.ClearOutput();
            _gameOutputService.ShowResults(_sessionResults);
            _gameOutputService.GameOver();
        }

        private void ReshuffleShoe()
        {
            _gameOutputService.ReshuffleShoe();
            _shoeService.ShuffleCards();
            _cardCountingService.ResetCount();
        }

        private void InitializeHands(int numberOfPlayerHands, decimal betAmount)
        {
            _dealerHand = new Hand();
            _playerHands.Clear();
            _finalHands.Clear();
            for (int i = 0; i < numberOfPlayerHands; i++)
            {
                _playerHands.Add(new Hand(betAmount));
            }
        }

        private void DealInitialCards()
        {
            _playerHands.ForEach(playerHand =>
            {
                playerHand.AddCard(_shoeService.DrawCard());
            });

            _dealerHand.AddCard(_shoeService.DrawCard());

            _playerHands.ForEach(playerHand =>
            {
                playerHand.AddCard(_shoeService.DrawCard());
            });

            _dealerHand.AddCard(_shoeService.DrawCard());
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
                        playerHand.AddCard(_shoeService.DrawCard());
                        break;
                    case PlayerAction.Stand:
                        return;
                    case PlayerAction.Double:
                        if (playerHand.CanDoubleDown)
                        {
                            playerHand.BetAmount *= 2;
                            playerHand.AddCard(_shoeService.DrawCard());
                            return;
                        }
                        else
                        {
                            _gameOutputService.InvalidAction();
                            continue;
                        }
                    case PlayerAction.Split:
                        if (playerHand.CanSplit)
                        {
                            var newHand = playerHand.Split();
                            playerHand.AddCard(_shoeService.DrawCard());
                            newHand.AddCard(_shoeService.DrawCard());
                            List<IHand> splitHands = new()
                            {
                                playerHand,
                                newHand
                            };
                            foreach (var splitHand in splitHands)
                            {
                                PlayHand(splitHand, dealerHand);

                                if (splitHand == newHand)
                                {
                                    _finalHands.Add(splitHand);
                                }
                            }
                            return;
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

            return validActions;
        }

        private void PlayDealerHand(IHand dealerHand)
        {
            while (dealerHand.GetTotal() < 17 && !dealerHand.IsBust)
            {
                dealerHand.AddCard(_shoeService.DrawCard());
            }
        }
    }
}
