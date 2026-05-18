using Moq;
using lab31v10;

namespace lab31v10.Tests;

public class InventoryServiceTests
{
    // хелпер щоб не дублювати створення моків
    private (InventoryService service, Mock<IInventoryRepository> repo, Mock<IAlertService> alert) CreateSut()
    {
        var repo = new Mock<IInventoryRepository>();
        var alert = new Mock<IAlertService>();
        var service = new InventoryService(repo.Object, alert.Object);
        return (service, repo, alert);
    }

    //ТЕСТ 1
    // Додавання товару — репозиторій має бути викликаний рівно один раз
    [Fact]
    public void AddItem_ValidItem_CallsRepositoryAdd()
    {
        var (service, repo, _) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Олівець", Quantity = 10 };

        service.AddItem(item);

        repo.Verify(r => r.Add(item), Times.Once);
    }

    // ТЕСТ 2
    // Якщо передати null — має кинути ArgumentNullException, Add не викликається
    [Fact]
    public void AddItem_NullItem_ThrowsArgumentNullException()
    {
        var (service, repo, _) = CreateSut();

        Assert.Throws<ArgumentNullException>(() => service.AddItem(null!));
        repo.Verify(r => r.Add(It.IsAny<InventoryItem>()), Times.Never);
    }

    //ТЕСТ 3
    // Якщо назва порожня — кидає ArgumentException
    [Fact]
    public void AddItem_EmptyName_ThrowsArgumentException()
    {
        var (service, _, _) = CreateSut();
        var item = new InventoryItem { Id = 2, Name = "", Quantity = 5 };

        Assert.Throws<ArgumentException>(() => service.AddItem(item));
    }

    //  ТЕСТ 4 
    // Успішне списання — повертає true і оновлює товар через Update
    [Fact]
    public void WriteOff_EnoughStock_ReturnsTrueAndCallsUpdate()
    {
        var (service, repo, _) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Зошит", Quantity = 20, LowStockThreshold = 5 };
        repo.Setup(r => r.GetById(1)).Returns(item);

        var result = service.WriteOff(1, 10);

        Assert.True(result);
        repo.Verify(r => r.Update(item), Times.Once);
    }

    //ТЕСТ 5 
    // Кількість падає до нуля — надсилається OutOfStock алерт
    [Fact]
    public void WriteOff_QuantityBecomesZero_SendsOutOfStockAlert()
    {
        var (service, repo, alert) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Ручка", Quantity = 3, LowStockThreshold = 5 };
        repo.Setup(r => r.GetById(1)).Returns(item);

        service.WriteOff(1, 3);

        alert.Verify(a => a.SendOutOfStockAlert("Ручка"), Times.Once);
        alert.Verify(a => a.SendLowStockAlert(It.IsAny<string>(), It.IsAny<int>()), Times.Never);
    }

    //ТЕСТ 6
    // Кількість падає нижче порогу (але не до нуля) — надсилається LowStock алерт
    [Fact]
    public void WriteOff_QuantityBelowThreshold_SendsLowStockAlert()
    {
        var (service, repo, alert) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Папір", Quantity = 10, LowStockThreshold = 5 };
        repo.Setup(r => r.GetById(1)).Returns(item);

        service.WriteOff(1, 8); // залишиться 2, а поріг = 5

        alert.Verify(a => a.SendLowStockAlert("Папір", 2), Times.Once);
    }

    //ТЕСТ 7
    // Недостатньо товару — повертає false, Update і алерти не викликаються
    [Fact]
    public void WriteOff_NotEnoughStock_ReturnsFalse()
    {
        var (service, repo, alert) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Лінійка", Quantity = 2 };
        repo.Setup(r => r.GetById(1)).Returns(item);

        var result = service.WriteOff(1, 10);

        Assert.False(result);
        repo.Verify(r => r.Update(It.IsAny<InventoryItem>()), Times.Never);
        alert.Verify(a => a.SendOutOfStockAlert(It.IsAny<string>()), Times.Never);
    }

    //ТЕСТ 8
    // Товар не знайдений — повертає false
    [Fact]
    public void WriteOff_ItemNotFound_ReturnsFalse()
    {
        var (service, repo, _) = CreateSut();
        repo.Setup(r => r.GetById(99)).Returns((InventoryItem?)null);

        var result = service.WriteOff(99, 5);

        Assert.False(result);
    }

    //ТЕСТ 9
    // Restock — збільшує кількість і викликає Update
    [Fact]
    public void Restock_ValidItem_IncreasesQuantityAndCallsUpdate()
    {
        var (service, repo, _) = CreateSut();
        var item = new InventoryItem { Id = 1, Name = "Маркер", Quantity = 5 };
        repo.Setup(r => r.GetById(1)).Returns(item);

        service.Restock(1, 10);

        Assert.Equal(15, item.Quantity);
        repo.Verify(r => r.Update(item), Times.Once);
    }

    // ТЕСТ 10 
    // Restock — товар не знайдений — кидає InvalidOperationException
    [Fact]
    public void Restock_ItemNotFound_ThrowsInvalidOperationException()
    {
        var (service, repo, _) = CreateSut();
        repo.Setup(r => r.GetById(55)).Returns((InventoryItem?)null);

        Assert.Throws<InvalidOperationException>(() => service.Restock(55, 10));
    }

    //ТЕСТ 11
    // GetAll — повертає список з репозиторію
    [Fact]
    public void GetAll_ReturnsItemsFromRepository()
    {
        var (service, repo, _) = CreateSut();
        var items = new List<InventoryItem>
        {
            new() { Id = 1, Name = "Скотч", Quantity = 7 },
            new() { Id = 2, Name = "Степлер", Quantity = 3 }
        };
        repo.Setup(r => r.GetAll()).Returns(items);

        var result = service.GetAll();

        Assert.Equal(2, result.Count);
        repo.Verify(r => r.GetAll(), Times.Once);
    }
}