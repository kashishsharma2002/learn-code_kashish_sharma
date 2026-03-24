using  BankingManagementSystem.Models;

public interface ITransactionService
{
    void ProcessTransaction(Transaction transaction);
    void ProcessTransfer(int sourceAccountId, int destinationAccountId, decimal amount);
    IEnumerable<Transaction> GetAllTransactions();
}