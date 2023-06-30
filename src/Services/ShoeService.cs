namespace Blackjack.Services
{
    public class ShoeService : IShoeService
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly List<ICard> _cards;
        private int _currentIndex;

        public ShoeService(GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            _cards = new List<ICard>();
            _currentIndex = 0;
            InitializeShoe();
        }

        public ICard DrawCard()
        {
            if (_currentIndex >= _cards.Count)
            {
                throw new InvalidOperationException("Cannot draw a card from an empty shoe.");
            }

            var card = _cards[_currentIndex];
            _currentIndex++;
            return card;
        }

        private void InitializeShoe()
        {
            _cards.Clear(); // Clear the cards list before re-initializing

            for (var i = 0; i < _gameConfiguration.DecksInShoe; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        _cards.Add(new Card(rank, suit));
                    }
                }
            }

            ShuffleCards();
        }

        public void ShuffleCards()
        {
            _currentIndex = 0;
            var rng = new Random();

            for (var i = 0; i < _cards.Count; i++)
            {
                var randomIndex = rng.Next(i, _cards.Count);
                (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
            }
        }

        public bool NeedsReshuffling()
        {
            var penetrationRate = (double)_currentIndex / _cards.Count;
            return penetrationRate >= _gameConfiguration.PenetrationRateRange.Min && penetrationRate <= _gameConfiguration.PenetrationRateRange.Max;
        }
    }
}
