using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace BankingManagementSystem.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = new List<Transaction>();
    
    public void Add(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public IEnumerable<Transaction> GetAll()
    {
        return _transactions;
    }

}