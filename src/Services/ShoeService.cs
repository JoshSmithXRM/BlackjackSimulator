namespace Blackjack.Services
{
    public class ShoeService : IShoeService
    {
        private readonly GameConfiguration _gameConfiguration;
#pragma warning disable IDE0044 // Add readonly modifier
        private List<ICard> _cards;
#pragma warning restore IDE0044 // Add readonly modifier
        private int _reshuffleIndex;

        public ShoeService(GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            _cards = new List<ICard>();
            InitializeShoe();
        }

        public IReadOnlyList<ICard> Cards => _cards.AsReadOnly();

        public ICard DrawCard()
        {
            if (_cards.Count == 0)
            {
                throw new InvalidOperationException("Cannot draw a card from an empty shoe.");
            }

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        public void InitializeShoe()
        {
            _cards.Clear(); // Clear the cards list before re-initializing

            for (int i = 0; i < _gameConfiguration.DecksInShoe; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        _cards.Add(new Card(rank, suit));
                    }
                }
            }

            _reshuffleIndex = (int)(RandomRange(_gameConfiguration.PenetrationRateRange.Min, _gameConfiguration.PenetrationRateRange.Max) * _cards.Count);
            Reshuffle();
        }

        public bool NeedsReshuffling()
        {
            return _cards.Count <= _reshuffleIndex;
        }

        public void Reshuffle()
        {
            var rng = new Random();

            for (var i = 0; i < _cards.Count; i++)
            {
                var randomIndex = rng.Next(i, _cards.Count);
                (_cards[i], _cards[randomIndex]) = (_cards[randomIndex], _cards[i]);
            }
        }

        private static double RandomRange(double min, double max)
        {
            var rnd = new Random();
            return rnd.NextDouble() * (max - min) + min;
        }
    }
}
