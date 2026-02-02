using System.Text.RegularExpressions;

namespace BankingManagementSystem.Common;

public interface IInputReader
{

    string ReadRequiredString(string message);
    string ReadWithRegex(string message, string pattern, string errorMessage);
    DateTime ReadDate(string message);
    int? ReadInt(string message);
    decimal ReadDecimal(string message);
}
