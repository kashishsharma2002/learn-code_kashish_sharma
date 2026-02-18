using System;
using System.Collections.Generic;

public class PaymentProcessor
{
    private const decimal MIN_AMOUNT = 0.01m;
    private const decimal MAX_AMOUNT = 5000m;
    private const int MAX_RETRIES = 2;

    private const string PAYMENT_SUCCESS = "Payment successful";
    private const string PAYMENT_FAILED = "Payment failed";


    private Logger logger;
    private NotificationService notifier;
    private Dictionary<string, PaymentRecord> history;


    public PaymentProcessor(Logger logger, NotificationService notifier)
    {
        this.logger = logger;
        this.notifier = notifier;
        this.history = new Dictionary<string, PaymentRecord>();
    }

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        ValidatePaymentRequest(request);

        int numberOfAttempt = 0;

        while (numberOfAttempt < MAX_RETRIES)
        {
            try
            {
                ExecutePaymentRequest(request);
                RecordPaymentRequest(request);
                NotifySuccess(request);

                return new PaymentResult(
                    true,
                    PAYMENT_SUCCESS,
                    GenerateTransactionId()
                );
            }
            catch (PaymentException)
            {
                numberOfAttempt++;
                logger.Log($"Retry attempt: {numberOfAttempt}");
            }
        }

        return new PaymentResult(false, PAYMENT_FAILED, null);
    }

    private void ValidatePaymentRequest(PaymentRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerId))
        {
            throw new ArgumentException("Customer ID required");
        }

        if (request.Amount < MIN_AMOUNT)
        {
            throw new ArgumentException("Invalid amount");
        }
    }

    private void ExecutePaymentRequest(PaymentRequest request)
    {
        logger.Log($"Executing payment of {request.Amount}");

        if (request.Amount > MAX_AMOUNT)
        {
            throw new PaymentException("Limit exceeded");
        }
    }

    private void RecordPaymentRequest(PaymentRequest request)
    {
        history[GenerateTransactionId()] =
            new PaymentRecord(
                request.CustomerId,
                request.Amount,
                DateTime.Now
            );
    }

    private void NotifySuccess(PaymentRequest request)
    {
        notifier.Send(
            request.CustomerId,
            $"Payment of {request.Amount} processed"
        );
    }

    private string GenerateTransactionId()
    {
        return "TXN-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}
