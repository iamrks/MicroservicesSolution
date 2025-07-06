using BuildingBlocks.EventBus.Events;
using InventoryService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Consumers;

public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly InventoryDbContext _context;

    public OrderCreatedEventConsumer(InventoryDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var order = context.Message;

        foreach (var item in order.Items)
        {
            var inventoryItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == item.ProductId);

            if (inventoryItem != null)
            {
                inventoryItem.AvailableQuantity -= item.Quantity;
                if (inventoryItem.AvailableQuantity < 0)
                    inventoryItem.AvailableQuantity = 0;
            }
        }

        await _context.SaveChangesAsync();

        Console.WriteLine($"[Inventory] Updated stock for OrderId: {order.OrderId}");
    }
}
