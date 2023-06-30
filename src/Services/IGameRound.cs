namespace Blackjack.Services
{
    public interface IGameRound
    {
        List<HandResult> Play(int numberOfPlayerHands, decimal betAmount);
    }
}