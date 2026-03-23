namespace ATMWithdrawalFlow.Exceptions
{
    public class NetworkConnectionException : Exception
    {
        public NetworkConnectionException() : base("Network connection error") { }
    }
}