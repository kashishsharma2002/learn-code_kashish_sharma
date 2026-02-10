using BankingManagementSystem.Controllers;

namespace BankingManagementSystem.UserInterface;

public class CustomerMenu
{
    private readonly CustomerController _controller;

    public CustomerMenu(CustomerController controller)
    {
        _controller = controller;
    }

    public void ShowCustomerMenu()
    {
        Console.WriteLine("\n--- CUSTOMER MANAGEMENT ---");
        Console.WriteLine("1. Create Customer");
        Console.WriteLine("2. View Customer by ID");
        Console.WriteLine("3. View All Customers");
        Console.WriteLine("4. Close Customer");

        Console.Write("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _controller.CreateCustomer();
                break;
            case "2":
                _controller.ViewCustomerById();
                break;
            case "3":
                _controller.ViewAllCustomers();
                break;
            case "4":
                _controller.CloseCustomer();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}
