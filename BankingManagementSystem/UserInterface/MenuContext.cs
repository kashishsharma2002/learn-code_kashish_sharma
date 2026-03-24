using BankingManagementSystem.Controllers;

namespace BankingManagementSystem.UserInterface;

public class MenuContext
{
    public CustomerController Customer { get; }
    public AccountController Account { get; }
    public TransactionController Transaction { get; }
    public LoanController Loan { get; }

    public MenuContext(
        CustomerController customer,
        AccountController account,
        TransactionController transaction,
        LoanController loan)
    {
        Customer = customer;
        Account = account;
        Transaction = transaction;
        Loan = loan;
    }
}
