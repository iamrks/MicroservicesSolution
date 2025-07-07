using BuildingBlocks.EventBus.Events;
using MassTransit;

namespace NotificationService.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventConsumer> _logger ;

    public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var order = context.Message;

        _logger.LogInformation($"[📨 Notification] Order received: ID={order.OrderId}, User={order.UserId}");

        foreach (var item in order.Items)
        {
            _logger.LogInformation($"  - {item.ProductName} x {item.Quantity} @ {item.Price}");
        }

        _logger.LogInformation($"Total: ₹{order.TotalAmount}");

        // Simulate sending email
        await Task.Delay(1000);
    }
}