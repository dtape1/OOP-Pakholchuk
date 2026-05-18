namespace lab31v10;

public interface IInventoryRepository
{
    InventoryItem? GetById(int id);
    List<InventoryItem> GetAll();
    void Update(InventoryItem item);
    void Add(InventoryItem item);
}