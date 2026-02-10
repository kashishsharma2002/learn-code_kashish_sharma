namespace BankingManagementSystem.UserInterface;

public class MainMenu
{
    private readonly CustomerMenu _customerMenu;
    private readonly AccountMenu _accountMenu;
    private readonly TransactionMenu _transactionMenu;
    private readonly LoanMenu _loanMenu;

    public MainMenu(MenuContext context)
    {
        _customerMenu = new CustomerMenu(context.Customer);
        _accountMenu = new AccountMenu(context.Account);
        _transactionMenu = new TransactionMenu(context.Transaction);
        _loanMenu = new LoanMenu(context.Loan);
    }

    public void Show()
    {
        while (true)
        {
            Console.WriteLine("\n=== BANKING MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Customer Management");
            Console.WriteLine("2. Account Management");
            Console.WriteLine("3. Transactions");
            Console.WriteLine("4. Loans");
            Console.WriteLine("5. Exit");

            Console.Write("Choose option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _customerMenu.ShowCustomerMenu();
                    break;
                case "2":
                    _accountMenu.ShowAcountMenu();
                    break;
                case "3":
                    _transactionMenu.ShowTransactionMenu();
                    break;
                case "4":
                    _loanMenu.ShowLoanMenu();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
