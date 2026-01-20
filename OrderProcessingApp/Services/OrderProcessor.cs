using OrderProcessingApp.Interfaces;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Services;

public class OrderProcessor
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly IInventoryService _inventoryService;
    private readonly INotificationService _notificationService;
    private readonly IOrderRepository _orderRepository;
    private readonly OrderValidator _validator;

    public OrderProcessor(
        IPaymentGateway paymentGateway,
        IInventoryService inventoryService,
        INotificationService notificationService,
        IOrderRepository orderRepository,
        OrderValidator validator)
    {
        _paymentGateway = paymentGateway;
        _inventoryService = inventoryService;
        _notificationService = notificationService;
        _orderRepository = orderRepository;
        _validator = validator;
    }

    public async Task<OrderResult> ProcessOrderAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        if (!_validator.IsValid(order))
            return OrderResult.Invalid("Order validation failed");

        if (!await _inventoryService.CheckAvailability(order.Items))
            return OrderResult.Failed("Insufficient inventory");

        await _inventoryService.ReserveItems(order.Items);

        try
        {
            var payment = await _paymentGateway.ProcessPayment(
                order.CustomerId,
                order.TotalAmount,
                order.PaymentMethod);

            if (!payment.IsSuccessful)
            {
                await _inventoryService.ReleaseReservation(order.Items);
                return OrderResult.Failed(payment.ErrorMessage ?? "Payment failed");
            }

            order.TransactionId = payment.TransactionId;
            order.Status = OrderStatus.Paid;

            await _inventoryService.CommitReservation(order.Items);
            await _notificationService.SendOrderConfirmation(order);
            await _orderRepository.Save(order);

            return OrderResult.Success(payment.TransactionId!);
        }
        catch
        {
            // IMPORTANT: Release inventory reservation to prevent permanent locks
            await _inventoryService.ReleaseReservation(order.Items);
            throw;
        }
    }

    public async Task CancelOrderAsync(string orderId)
    {
        var order = await _orderRepository.GetById(orderId)
            ?? throw new ArgumentException($"Order {orderId} not found");

        if (order.Status == OrderStatus.Paid && order.TransactionId != null)
        {
            await _paymentGateway.RefundPayment(order.TransactionId);
            await _inventoryService.RestoreInventory(order.Items);
        }

        order.Status = OrderStatus.Cancelled;
        await _orderRepository.Save(order);
    }
}
