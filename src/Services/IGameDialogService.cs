namespace Blackjack.Services
{
    public interface IGameDialogService
    {
        PlayerAction GetPlayerAction(List<PlayerAction> availableActions);
        int GetNumberOfHands();
        decimal GetWagerAmount();
    }
}