namespace lab31v10;

public interface IAlertService
{
    void SendLowStockAlert(string itemName, int quantity);
    void SendOutOfStockAlert(string itemName);
}