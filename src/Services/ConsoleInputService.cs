
namespace Blackjack.Services
{
    public class ConsoleInputService : IInputService
    {
        public decimal ReadDecimalInput()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                {
                    return -1;
                }
                if (decimal.TryParse(input, out decimal value))
                {
                    return value;
                }
            }
        }

        public string ReadInput()
        {
            var input = Console.ReadLine();
            if (input == null)
            {
                return string.Empty;
            }
            return input;
        }

        public int ReadIntegerInput()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == null)
                {
                    return -1;
                }
                if (int.TryParse(input, out int value))
                {
                    return value;
                }
            }
        }
    }
}
