namespace Blackjack.Services
{
    public interface IGameInputService
    {
        PlayerAction GetPlayerAction(List<PlayerAction> availableActions);
        int GetNumberOfHands();
        decimal GetWagerAmount();
    }
}