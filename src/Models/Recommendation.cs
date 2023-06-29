namespace Blackjack.Models
{
    public class Recommendation
    {
        public int RunningCount { get; set; }
        public CountType CountType { get; set; }
        public PlayerAction? Action { get; set; }

        public Recommendation(int runningCount, CountType countType, PlayerAction? action)
        {
            RunningCount = runningCount;
            CountType = countType;
            Action = action;
        }
    }
}