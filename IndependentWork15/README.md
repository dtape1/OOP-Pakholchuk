# Звіт з аналізу SOLID принципів (SRP, OCP) в Open-Source проєкті

 1. Обраний проєкт

- Назва: Entity Framework Core
- Посилання на GitHub: https://github.com/dotnet/efcore

Я вибрав Entity Framework Core, тому що це великий open-source проєкт на C#, в якому є багато класів, інтерфейсів і реальної бізнес-логіки. На ньому добре видно, як застосовуються принципи SOLID на практиці.



 2. Аналіз SRP (Single Responsibility Principle)

 2.1. Приклади дотримання SRP

 Клас: DbContext

- Відповідальність: керування сесією роботи з базою даних
- Обґрунтування: клас відповідає тільки за взаємодію з БД (запити, збереження змін), а не за бізнес-логіку або UI

```csharp
public abstract class DbContext : IDisposable
{
    public virtual int SaveChanges()
    {
        return SaveChanges(true);
    }
}
```

Цей клас не займається валідацією, логуванням чи відображенням даних, а лише роботою з контекстом БД, тому він добре дотримується SRP.


 Клас: DbSet<TEntity>

- Відповідальність: робота з конкретною таблицею (сутністю)
- Обґрунтування: клас інкапсулює тільки CRUD-операції для однієї сутності

```csharp
public abstract class DbSet<TEntity> where TEntity : class
{
    public abstract void Add(TEntity entity);
    public abstract void Remove(TEntity entity);
}
```

DbSet не знає нічого про підключення до БД або бізнес-правила, він відповідає лише за одну сутність.


 Клас: ModelBuilder

- Відповідальність: конфігурація моделі даних
- Обґрунтування: клас використовується тільки для налаштування зв’язків, ключів та властивостей

```csharp
public class ModelBuilder
{
    public virtual EntityTypeBuilder<TEntity> Entity<TEntity>()
        where TEntity : class
    {
        return new EntityTypeBuilder<TEntity>();
    }
}
```

Цей клас не виконує запити і не працює з даними напряму, а лише конфігурує модель.


 2.2. Приклади порушення SRP

 Клас: MigrationsScaffolder

- Множинні відповідальності:
  - генерація міграцій
  - робота з файловою системою
  - форматування коду
- Проблеми: зміна одного аспекту може зламати інший

```csharp
public class MigrationsScaffolder
{
    public virtual ScaffoldedMigration ScaffoldMigration(
        string migrationName,
        string rootNamespace)
    {
        // генерація коду міграції
        // робота з файлами
        // логіка форматування
    }
}
```

Клас виконує забагато різних задач, через що його складно підтримувати і тестувати.



 Клас: RelationalDatabaseCreator

- Множинні відповідальності:
  - створення БД
  - перевірка існування
  - ініціалізація структури
- Проблеми: при зміні логіки створення БД потрібно змінювати вже існуючий код

```csharp
public class RelationalDatabaseCreator
{
    public virtual bool Exists()
    {
        // перевірка існування БД
    }

    public virtual void Create()
    {
        // створення БД
    }
}
```

Тут краще було б розділити логіку на декілька класів.


 3. Аналіз OCP (Open/Closed Principle)

 3.1. Приклади дотримання OCP

 Сценарій: Провайдери баз даних

- Механізм розширення: інтерфейси та абстрактні класи
- Обґрунтування: можна додати новий тип БД без зміни існуючого коду

```csharp
public interface IDatabaseProvider
{
    string Name { get; }
}
```

```csharp
public class SqlServerDatabaseProvider : IDatabaseProvider
{
    public string Name => "SQL Server";
}
```

Щоб додати нову БД, достатньо створити нову реалізацію інтерфейсу.


 Сценарій: Логування

- Механізм розширення: Dependency Injection
- Обґрунтування: можна підключити інший логер без редагування коду EF Core

```csharp
public interface ILogger
{
    void Log(string message);
}
```

EF Core працює з інтерфейсом, а не з конкретною реалізацією.


 Сценарій: Стратегії міграцій

- Механізм розширення: патерн Strategy
- Обґрунтування: нові стратегії додаються без змін існуючих

```csharp
public interface IMigrationCommandExecutor
{
    void Execute();
}
```


 3.2. Приклади порушення OCP

 Сценарій: Вибір типу операції

- Проблема: використання switch
- Наслідки: при додаванні нового типу потрібно змінювати код

```csharp
switch (operationType)
{
    case "Add":
        AddEntity();
        break;
    case "Update":
        UpdateEntity();
        break;
    case "Delete":
        DeleteEntity();
        break;
}
```

Кожне нове значення змушує редагувати існуючий код, що порушує OCP.


 Сценарій: Форматування SQL

- Проблема: жорстка прив’язка до типів
- Наслідки: код важко розширювати

```csharp
if (provider == "SqlServer")
{
    FormatSqlServer();
}
else if (provider == "Sqlite")
{
    FormatSqlite();
}
```

Тут краще було б використати інтерфейс або фабрику.

 4. Загальні висновки

Під час аналізу Entity Framework Core видно, що більшість архітектурних рішень відповідають принципам SRP та OCP. Особливо добре реалізовано OCP через використання інтерфейсів, абстракцій та dependency injection.  
Водночас у великих сервісних класах інколи порушується SRP, що ускладнює підтримку коду. Загалом дизайн проєкту якісний і показує правильне застосування принципів SOLID у реальному open-source проєкті.
