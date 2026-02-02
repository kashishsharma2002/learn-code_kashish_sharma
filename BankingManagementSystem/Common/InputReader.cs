using System.Text.RegularExpressions;

namespace BankingManagementSystem.Common;

public class InputReader : IInputReader
{

    public string ReadRequiredString(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("This field is required. Please try again.");
        }
    }

    public string ReadWithRegex(string message, string pattern, string errorMessage)
    {
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine() ?? string.Empty;

            if (Regex.IsMatch(input, pattern))
                return input;

            Console.WriteLine(errorMessage);
        }
    }

    public DateTime ReadDate(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            if (DateTime.TryParse(Console.ReadLine(), out var date))
                return date;

            Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
        }
    }

    public int? ReadInt(string message)
    {
        var input = ReadRequiredString(message);
        if (!int.TryParse(input, out var value))
        {
            Console.WriteLine("Invalid number");
            return null;
        }
        return value;
    }

    public decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            if (decimal.TryParse(Console.ReadLine(), out var value))
                return value;

            Console.WriteLine("Invalid decimal number. Please try again.");
        }
    }
}