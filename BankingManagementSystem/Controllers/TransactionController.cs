using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;

namespace BankingManagementSystem.Controllers;

public class TransactionController
{
    private readonly ITransactionService _transactionService;
    private readonly IInputReader _inputReader;

    public TransactionController(ITransactionService transactionService, IInputReader inputReader)
    {
        _transactionService = transactionService;
        _inputReader = inputReader;
    }

    public void ProcessTransaction()
    {
        try
        {
            var accountId = _inputReader.ReadInt("Enter Account ID:");
            if (!accountId.HasValue)
                throw new InvalidOperationException("Account ID is required");

            var transactionType = ReadTransactionType("Enter Transaction Type (Deposit/Withdrawal/Transfer):");

            if (transactionType == TransactionType.Transfer)
            {
                ProcessTransferTransaction(accountId.Value);
            }
            else
            {
                var amount = _inputReader.ReadDecimal("Enter Amount:");
                var paymentMethod = ReadPaymentMethod("Enter Payment Method:");

                var transaction = new Transaction
                {
                    AccountId = accountId.Value,
                    Amount = amount,
                    TransactionType = transactionType,
                    PaymentMethod = paymentMethod,
                    TransactionDate = DateTime.Now
                };

                _transactionService.ProcessTransaction(transaction);
                Console.WriteLine("Transaction processed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing transaction: {ex.Message}");
        }
    }

    private void ProcessTransferTransaction(int sourceAccountId)
    {
        try
        {
            var destinationAccountId = _inputReader.ReadInt("Enter Destination Account ID:");
            if (!destinationAccountId.HasValue)
                throw new InvalidOperationException("Destination Account ID is required");

            var amount = _inputReader.ReadDecimal("Enter Transfer Amount:");

            _transactionService.ProcessTransfer(sourceAccountId, destinationAccountId.Value, amount);
            Console.WriteLine("Transfer completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing transfer: {ex.Message}");
        }
    }

    private TransactionType ReadTransactionType(string prompt)
    {
        while (true)
        {
            var input = _inputReader.ReadRequiredString(prompt);
            if (Enum.TryParse<TransactionType>(input, true, out var transactionType))
                return transactionType;

            Console.WriteLine("Invalid transaction type. Please enter Deposit, Withdrawal, or Transfer.");
        }
    }

    private PaymentMethod ReadPaymentMethod(string prompt)
    {
        while (true)
        {
            var input = _inputReader.ReadRequiredString(prompt);
            if (Enum.TryParse<PaymentMethod>(input, true, out var paymentMethod))
                return paymentMethod;

            Console.WriteLine("Invalid payment method. Please enter Netbanking, DebitCard, CreditCard, Cheque, or UPI.");
        }
    }

    public void ViewAllTransactions()
    {
        try
        {
            var transactions = _transactionService.GetAllTransactions();
            foreach (var transaction in transactions)
            {
                DisplayTransaction(transaction);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving transactions: {ex.Message}");
        }
    }

    private void DisplayTransaction(Transaction transaction)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Transaction ID: {transaction.TransactionId}");
        Console.WriteLine($"Account ID: {transaction.AccountId}");
        Console.WriteLine($"Amount: {transaction.Amount}");
        Console.WriteLine($"Transaction Type: {transaction.TransactionType}");
        Console.WriteLine($"Payment Method: {transaction.PaymentMethod}");
        Console.WriteLine($"Transaction Date: {transaction.TransactionDate}");
        Console.WriteLine("-----------------------");
    }
}