using BankingManagementSystem.Controllers;

namespace BankingManagementSystem.UserInterface;

public class LoanMenu
{
    private readonly LoanController _controller;

    public LoanMenu(LoanController controller)
    {
        _controller = controller;
    }

    public void ShowLoanMenu()
    {
        Console.WriteLine("\n--- LOAN MANAGEMENT ---");
        Console.WriteLine("1. Apply for Loan");
        Console.WriteLine("2. View Loan by ID");
        Console.Write("Choose option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _controller.ApplyLoanApplication();
                break;
            case "2":
                _controller.ViewLoanById();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}
