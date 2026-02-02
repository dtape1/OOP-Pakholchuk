Домашня робота №3. Принципи ISP та DIP
Принцип ISP (Interface Segregation Principle) говорить, що інтерфейси не повинні змушувати класи реалізовувати методи, які їм не потрібні. Якщо один інтерфейс робить забагато речей, його реалізації стають громіздкими і складними для підтримки
Приклад порушення ISP:

```csharp
public interface IPrinter
{
    void PrintDocument(string content)
    void ScanDocument(string content)
    void FaxDocument(string content)
}
```

Тут якщо ми хочемо зробити тільки принтер без сканера і факсу, нам все одно треба реалізовувати порожні методи ScanDocument і FaxDocument. Це незручно і порушує ISP
Виправлення через "вузькі" інтерфейси:

```csharp
public interface IPrinter
{
    void PrintDocument(string content)
}

public interface IScanner
{
    void ScanDocument(string content)
}

public interface IFax
{
    void FaxDocument(string content)
}
```

Тепер клас, який реалізує тільки друк, має реалізовувати тільки метод PrintDocument. Код стає простішим для підтримки та тестування
Принцип DIP (Dependency Inversion Principle) говорить, що класи повинні залежати від абстракцій, а не від конкретних реалізацій. Це допомагає легко змінювати залежності і використовувати Dependency Injection
Приклад DIP через Dependency Injection:

```csharp
public interface IMessageService
{
    void SendMessage(string message)
}

public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Sending email: " + message)
    }
}

public class Notification
{
    private IMessageService _messageService

    public Notification(IMessageService messageService)
    {
        _messageService = messageService
    }

    public void SendNotification(string message)
    {
        _messageService.SendMessage(message)
    }
}
```

Тут клас Notification не знає, як саме надсилається повідомлення, він працює через інтерфейс IMessageService. Можемо підставити EmailService, SMSService або будь-який інший сервіс без змін Notification. Це робить код гнучким, легким для тестування і дозволяє міняти реалізації без правок основного класу
Висновок:
Вузькі інтерфейси (ISP) дозволяють реалізовувати тільки потрібні методи, що робить код чистішим і зручним для тестування
Dependency Injection та DIP допомагають відокремити залежності і роблять програму гнучкою до змін