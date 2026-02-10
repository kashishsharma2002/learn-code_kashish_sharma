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

        ApplyTransactionEffect(account, transaction);

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

        EnsureSufficientBalance(sourceAccount, amount);

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

    public IEnumerable<Transaction> GetAllTransactions()
    {
        var transactions = _transactionRepository.GetAll();

        if (!transactions.Any())
            throw new InvalidOperationException("No transactions found");

        return transactions;
    }

    private void ApplyTransactionEffect(Account account, Transaction transaction)
    {
        switch (transaction.TransactionType)
        {
            case TransactionType.Deposit:
                account.Balance += transaction.Amount;
                break;

            case TransactionType.Withdrawal:
                Withdraw(account, transaction.Amount);
                break;

            default:
                throw new InvalidOperationException("Transfers must be processed using ProcessTransfer");
        }
    }

    private static void  Withdraw(Account account, decimal amount)
    {
        EnsureSufficientBalance(account, amount);

        account.Balance -= amount;
    }

    private static void EnsureSufficientBalance(Account account, decimal amount)
    {
        if (account.Balance < amount)
            throw new InsufficientFundsException(amount, account.Balance);
    }
}