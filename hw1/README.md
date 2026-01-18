God Object — це антипатерн, коли один клас робить занадто багато всього. Такий клас містить багато логіки, яка не відноситься до однієї конкретної задачі. Через це код стає важким для читання і підтримки, а будь-які зміни можуть зачепити одразу кілька частин програми.

Зазвичай God Object порушує принцип єдиної відповідальності (SRP), тому що клас замість однієї задачі виконує одразу кілька. Наприклад, він може працювати з даними, виконувати бізнес-логіку, зберігати інформацію та ще й відправляти повідомлення.

Приклад простого класу, який порушує SRP:

public class UserManager
{
    public void RegisterUser(string username, string email)
    {
        Console.WriteLine("Validating email...");
        Console.WriteLine("Saving user to database...");
        Console.WriteLine("Sending welcome email...");
        Console.WriteLine("Logging user registration...");
    }
}

У цьому прикладі клас UserManager робить одразу кілька різних речей: перевіряє дані, зберігає користувача в базу даних, відправляє email та веде логування. Це означає, що клас має кілька відповідальностей і порушує принцип SRP. Якщо зміниться логіка відправки email або спосіб збереження даних, доведеться змінювати цей самий клас.

Щоб виправити проблему і дотримуватись SRP, логіку потрібно розділити на окремі класи:

public class UserValidator
{
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}

public class UserRepository
{
    public void Save(string username, string email)
    {
        Console.WriteLine("Saving user to database...");
    }
}

public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
        Console.WriteLine("Sending welcome email...");
    }
}

Після цього кожен клас відповідає тільки за одну задачу. Код стає простішим, зрозумілішим і його легше змінювати та тестувати. Такий підхід відповідає принципу єдиної відповідальності і допомагає уникати антипатерну God Object.