using BankingManagementSystem.Controllers;

namespace BankingManagementSystem.UserInterface;

public class TransactionMenu
{
    private readonly TransactionController _controller;

    public TransactionMenu(TransactionController controller)
    {
        _controller = controller;
    }

    public void ShowTransactionMenu()
    {
        Console.WriteLine("\n--- TRANSACTIONS ---");
        Console.WriteLine("1. Process Transaction");
        Console.WriteLine("2. View All Transactions");
        Console.Write("Choose option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _controller.ProcessTransaction();
                break;
            case "2":
                _controller.ViewAllTransactions();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}
