using BuildingBlocks.EventBus.Events;
using MassTransit;

namespace BasketService.Consumers;

public class ProductUpdatedEventConsumer : IConsumer<ProductUpdatedEvent>
{
    public Task Consume(ConsumeContext<ProductUpdatedEvent> context)
    {
        var message = context.Message;
        Console.WriteLine($"Product update received in Basket: {message.ProductId}, New Price: {message.NewPrice}");
        // TODO: update price in cached basket if needed
        return Task.CompletedTask;
    }
}
