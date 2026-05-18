namespace lab31v10;

public class InventoryService
{
    private readonly IInventoryRepository _repo;
    private readonly IAlertService _alert;

    public InventoryService(IInventoryRepository repo, IAlertService alert)
    {
        _repo = repo;
        _alert = alert;
    }

    // Додати новий товар
    public void AddItem(InventoryItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        if (string.IsNullOrWhiteSpace(item.Name))
            throw new ArgumentException("Назва не може бути порожньою");

        _repo.Add(item);
    }

    // Списати кількість зі складу
    public bool WriteOff(int itemId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше 0");

        var item = _repo.GetById(itemId);
        if (item == null)
            return false;

        if (item.Quantity < quantity)
            return false;

        item.Quantity -= quantity;
        _repo.Update(item);

        // перевіряємо чи треба слати алерт
        if (item.Quantity == 0)
            _alert.SendOutOfStockAlert(item.Name);
        else if (item.Quantity <= item.LowStockThreshold)
            _alert.SendLowStockAlert(item.Name, item.Quantity);

        return true;
    }

    // Поповнити запас
    public void Restock(int itemId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше 0");

        var item = _repo.GetById(itemId);
        if (item == null)
            throw new InvalidOperationException("Товар не знайдено");

        item.Quantity += quantity;
        _repo.Update(item);
    }

    // Отримати всі товари
    public List<InventoryItem> GetAll()
    {
        return _repo.GetAll();
    }
}