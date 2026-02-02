using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private static int _transactionIdCounter = 1;

    public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public void ProcessTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction, nameof(transaction));

        if (transaction.Amount <= 0)
            throw new ArgumentException("Transaction amount must be positive");

        var account = _accountRepository.GetById(transaction.AccountId);
        if (account == null)
            throw new AccountNotFoundException(transaction.AccountId);

        transaction.TransactionId = _transactionIdCounter++;
        transaction.TransactionDate = DateTime.Now;

        switch (transaction.TransactionType)
        {
            case TransactionType.Withdrawal:
                ProcessWithdrawal(account, transaction.Amount);
                break;
            case TransactionType.Deposit:
                account.Balance += transaction.Amount;
                break;
            case TransactionType.Transfer:
                ProcessWithdrawal(account, transaction.Amount);
                break;
            default:
                throw new ArgumentException("Invalid transaction type");
        }

        _accountRepository.Update(account);
        _transactionRepository.Add(transaction);
    }

    public void ProcessTransfer(int sourceAccountId, int destinationAccountId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be positive");

        var sourceAccount = _accountRepository.GetById(sourceAccountId);
        var destinationAccount = _accountRepository.GetById(destinationAccountId);

        if (sourceAccount == null)
            throw new AccountNotFoundException(sourceAccountId);
        if (destinationAccount == null)
            throw new AccountNotFoundException(destinationAccountId);

        if (sourceAccount.Balance < amount)
            throw new InsufficientFundsException(amount, sourceAccount.Balance);

        sourceAccount.Balance -= amount;
        destinationAccount.Balance += amount;

        _accountRepository.Update(sourceAccount);
        _accountRepository.Update(destinationAccount);

        var transaction = new Transaction
        {
            TransactionId = _transactionIdCounter++,
            AccountId = sourceAccountId,
            Amount = amount,
            TransactionType = TransactionType.Transfer,
            PaymentMethod = PaymentMethod.Netbanking,
            TransactionDate = DateTime.Now
        };

        _transactionRepository.Add(transaction);
    }

    private void ProcessWithdrawal(Account account, decimal amount)
    {
        if (account.Balance < amount)
            throw new InsufficientFundsException(amount, account.Balance);

        account.Balance -= amount;
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        var transactionsList = _transactionRepository.GetAll();
        if (!transactionsList.Any())
            throw new System.InvalidOperationException("No transactions found");
        return transactionsList;
    }
}