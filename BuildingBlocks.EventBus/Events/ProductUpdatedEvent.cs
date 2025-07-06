namespace BuildingBlocks.EventBus.Events;

public class ProductUpdatedEvent
{
    public int ProductId { get; set; }
    public string NewName { get; set; } = string.Empty;
    public decimal NewPrice { get; set; }
}
