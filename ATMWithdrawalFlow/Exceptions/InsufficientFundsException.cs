namespace ATMWithdrawalFlow.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient funds") { }
    }
}