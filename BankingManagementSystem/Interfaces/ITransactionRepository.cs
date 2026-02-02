using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface ITransactionRepository
{
    void Add(Transaction transaction);
    IEnumerable<Transaction> GetAll();
    IEnumerable<Transaction> GetTransactionByCustomerId(int customerId);
}