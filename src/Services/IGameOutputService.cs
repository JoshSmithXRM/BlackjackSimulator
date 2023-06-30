namespace Blackjack.Services
{
    public interface IGameOutputService
    {
        void ClearOutput();
        void InvalidAction();
        void ReshuffleShoe();
        void GameOver();
        void Recommendation(Recommendation recommendation);
        void ShowResults(List<HandResult> results);
        void ShowHands(IHand playerHand, IHand dealerHand, bool hideSecondCard = false);
        void ServiceMissing(string serviceName);
        void SimulationStarted();
        void RoundCompleted(int roundNumber, int totalRounds);
    }
}