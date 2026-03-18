namespace WalletPaymentSystem.Models
{
    public sealed class PaymentReceipt
    {
        public PaymentReceipt(bool wasPaid, decimal amount, decimal balanceBefore, decimal balanceAfter)
        {
            WasPaid = wasPaid;
            Amount = amount;
            BalanceBefore = balanceBefore;
            BalanceAfter = balanceAfter;
        }

        public bool WasPaid { get; }
        public decimal Amount { get; }
        public decimal BalanceBefore { get; }
        public decimal BalanceAfter { get; }
    }
}