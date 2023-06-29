namespace Blackjack.Services
{
    public interface IInputService
    {
        decimal ReadDecimalInput();
        string ReadInput();
        int ReadIntegerInput();
    }
}