using OrderProcessingApp.Models;

namespace OrderProcessingApp.Services
{
    public class OrderValidator
    {
        public bool IsValid(Order order)
        {
            if (order.Items == null || order.Items.Count == 0)
                return false;

            if (order.TotalAmount <= 0)
                return false;

            // NOTE: Additional validation rules required in production:
            // - Customer credit limit validation (prevents over-spending)
            // - Payment method verification (ensures valid payment source)
            // - Shipping address validation (prevents delivery failures)
            return true;
        }
    }
}
