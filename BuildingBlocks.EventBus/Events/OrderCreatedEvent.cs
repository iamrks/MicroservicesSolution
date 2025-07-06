namespace BuildingBlocks.EventBus.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<OrderItemEventDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

public class OrderItemEventDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
