namespace Blackjack.Services
{
    public class GameService
    {
        private readonly IShoeService _shoeService;
        private readonly ICardCountingService _cardCountingService;
        private readonly IGameOutputService _gameOutputService;
        private readonly GameConfiguration _gameConfiguration;
        private readonly List<Hand> _playerHands;
        private readonly Hand _dealerHand;

        public GameService(
            IShoeService shoeService,
            ICardCountingService cardCountingService,
            IGameOutputService gameOutputService,
            GameConfiguration gameConfiguration)
        {
            _shoeService = shoeService;
            _cardCountingService = cardCountingService;
            _gameOutputService = gameOutputService;
            _gameConfiguration = gameConfiguration;
            _playerHands = new List<Hand>();
            _dealerHand = new Hand();
        }

        public void PlayGame()
        {
            while (true)
            {
                _gameOutputService.NewHand();

                if (_shoeService.NeedsReshuffling())
                {
                    _gameOutputService.ReshuffleShoe();
                    _shoeService.InitializeShoe();
                }

                DealInitialCards();

                if (CheckForBlackjack())
                {
                    EndRound();
                    continue;
                }

                HandlePlayerTurns();

                if (!CheckPlayerBusts())
                {
                    HandleDealerTurn();
                    DetermineRoundWinner();
                }

                _playerHands.Clear();
                _dealerHand.Clear();

                if (!PlayAnotherHand())
                    break;
            }
        }

        private void DealInitialCards()
        {
            for (int i = 0; i < _gameConfiguration.NumberOfPlayerHands; i++)
            {
                var playerHand = new Hand();
                playerHand.AddCard(_shoeService.DrawCard());
                playerHand.AddCard(_shoeService.DrawCard());
                _playerHands.Add(playerHand);
                _gameOutputService.PlayerHand(playerHand.PartialString());
            }

            _dealerHand.AddCard(_shoeService.DrawCard());
            _dealerHand.AddCard(_shoeService.DrawCard(true));
            _gameOutputService.DealerHand(_dealerHand.PartialString(true));
        }

        private bool CheckForBlackjack()
        {
            if (_playerHands.Any(hand => hand.IsBlackjack) && _dealerHand.IsBlackjack)
            {
                _gameOutputService.Push();
                return true;
            }
            else if (_playerHands.Any(hand => hand.IsBlackjack))
            {
                _gameOutputService.PlayerWinsWithBlackjack();
                return true;
            }
            else if (_dealerHand.IsBlackjack)
            {
                _gameOutputService.DealerWinsWithBlackjack();
                return true;
            }

            return false;
        }

        private void HandlePlayerTurns()
        {
            for (int i = 0; i < _playerHands.Count; i++)
            {
                var playerHand = _playerHands[i];

                while (true)
                {
                    var playerAction = _gameOutputService.GetPlayerAction();

                    if (playerAction == PlayerAction.Hit)
                    {
                        playerHand.AddCard(_shoeService.DrawCard());
                        _gameOutputService.PlayerHand(playerHand.ToString());

                        if (playerHand.IsBust)
                        {
                            _gameOutputService.PlayerBusts();
                            break;
                        }

                        _gameOutputService.Recommendation(_cardCountingService.GetRecommendation(0, playerHand, _dealerHand.Cards[0]));
                    }
                    else if (playerAction == PlayerAction.Stand)
                    {
                        break;
                    }
                    else if (playerAction == PlayerAction.Double)
                    {
                        playerHand.AddCard(_shoeService.DrawCard());
                        _gameOutputService.PlayerHand(playerHand.ToString());

                        if (playerHand.IsBust)
                        {
                            _gameOutputService.PlayerBusts();
                            break;
                        }

                        _gameOutputService.Recommendation(_cardCountingService.GetRecommendation(0, playerHand, _dealerHand.Cards[0]));
                        break;
                    }
                }
            }
        }

        private bool CheckPlayerBusts()
        {
            bool anyBusts = false;

            for (int i = 0; i < _playerHands.Count; i++)
            {
                var playerHand = _playerHands[i];

                if (playerHand.IsBust)
                {
                    _gameOutputService.PlayerBusts();
                    anyBusts = true;
                }
            }

            return anyBusts;
        }

        private void HandleDealerTurn()
        {
            while (_dealerHand.GetTotal() < 17)
            {
                _dealerHand.AddCard(_shoeService.DrawCard());
                _gameOutputService.DealerHand(_dealerHand.ToString());

                if (_dealerHand.IsBust)
                {
                    _gameOutputService.DealerBusts();
                    break;
                }
            }
        }

        private void DetermineRoundWinner()
        {
            var dealerTotal = _dealerHand.GetTotal();

            for (int i = 0; i < _playerHands.Count; i++)
            {
                var playerHand = _playerHands[i];
                var playerTotal = playerHand.GetTotal();

                if (playerTotal > dealerTotal)
                    _gameOutputService.PlayerWins();
                else if (playerTotal < dealerTotal)
                    _gameOutputService.DealerWins();
                else
                    _gameOutputService.Tie();
            }
        }

        private bool PlayAnotherHand()
        {
            _gameOutputService.WriteLine("Do you want to play another hand? (Y/N)");

            while (true)
            {
                var input = Console.ReadLine();

                if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                _gameOutputService.InvalidInput();
            }
        }
    }
}
