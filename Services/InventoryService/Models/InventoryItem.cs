namespace InventoryService.Models;

public class InventoryItem
{
    public int Id { get; set; }           // ProductId
    public string ProductName { get; set; } = string.Empty;
    public int AvailableQuantity { get; set; }
}
