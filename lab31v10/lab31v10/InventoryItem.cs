namespace lab31v10;

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public int LowStockThreshold { get; set; } = 5;
}