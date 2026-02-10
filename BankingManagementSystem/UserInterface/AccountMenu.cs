using BankingManagementSystem.Controllers;

namespace BankingManagementSystem.UserInterface;

public class AccountMenu
{
    private readonly AccountController _controller;

    public AccountMenu(AccountController controller)
    {
        _controller = controller;
    }

    public void ShowAcountMenu()
    {
        Console.WriteLine("\n--- ACCOUNT MANAGEMENT ---");
        Console.WriteLine("1. Create Customer Account");
        Console.WriteLine("2. View Customer Accounts");
        Console.WriteLine("3. View All Accounts");
        Console.WriteLine("4. Close Account");
        Console.Write("Choose option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _controller.CreateCustomerAccount();
                break;
            case "2":
                _controller.GetAccountDetails();
                break;
            case "3":
                _controller.GetAllAccountsDetails();
                break;
            case "4":
                _controller.CloseCustomerAccount();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}
