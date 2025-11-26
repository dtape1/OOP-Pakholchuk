using lab6v10;
using System.Linq;

namespace lab6v10;

internal class Program
{
    // Власний делегат для простої арифметики
    public delegate int IntOperation(int x, int y);

    static void Main(string[] args)
    {
        // 1. Власний делегат + анонімний метод 

        // Анонімний метод
        IntOperation addAnon = delegate (int a, int b)
        {
            return a + b;
        };

        // Лямбда-вираз для того ж делегата
        IntOperation mulLambda = (a, b) => a * b;

        Console.WriteLine("Анонімний addAnon(2, 3) = " + addAnon(2, 3));
        Console.WriteLine("Лямбда mulLambda(4, 5) = " + mulLambda(4, 5));

        Console.WriteLine(new string('-', 40));

        // 2. Стандартні делегати: Action / Func / Predicate 

        // Action<string> – просто щось вивести
        Action<string> log = msg => Console.WriteLine("[LOG] " + msg);

        // Func<Book, double> – витягуємо рейтинг
        Func<Book, double> getRating = b => b.Rating;

        // Predicate<Book> – перевірка по рейтингу
        Predicate<Book> isTopRated = b => b.Rating > 4.5;

        log("Приклади делегатів на списку книг");
        Console.WriteLine(new string('-', 40));

        // 3. Колекція книг (дані для прикладу) 

        var books = new List<Book>
        {
            new Book("Чистий код", "Р. Мартін", 2008, 4.8),
            new Book("Патерни проєктування", "ГоФ", 1994, 4.6),
            new Book("CLR via C#", "Д. Ріхтер", 2012, 4.7),
            new Book("1984", "Дж. Орвелл", 1949, 4.9),
            new Book("Колгосп тварин", "Дж. Орвелл", 1945, 4.4),
            new Book("Пригоди Тома Сойєра", "М. Твен", 1876, 4.3)
        };

        //4. Вибір книг з рейтингом > 4.5 (Where + Predicate) 

        var highRated = books.Where(b => isTopRated(b));
        log("Книги з рейтингом > 4.5:");

        foreach (var b in highRated)
            Console.WriteLine(b);

        Console.WriteLine(new string('-', 40));

        // 5. Групування за автором (GroupBy) 

        log("Групування за автором:");

        var groupedByAuthor = books
            .GroupBy(b => b.Author)
            .OrderBy(g => g.Key); // для краси

        foreach (var group in groupedByAuthor)
        {
            Console.WriteLine($"Автор: {group.Key}");
            foreach (var b in group)
                Console.WriteLine("  " + b);
        }

        Console.WriteLine(new string('-', 40));

        // 6. Сортування за роком видання (OrderBy)

        log("Сортування за роком (від старих до нових):");

        var orderedByYear = books.OrderBy(b => b.Year);

        foreach (var b in orderedByYear)
            Console.WriteLine(b);

        Console.WriteLine(new string('-', 40));

        // 7. Обчислення: середній рейтинг + сума рейтингів 

        double avgRating = books.Average(getRating);
        Console.WriteLine($"Середній рейтинг усіх книг: {avgRating:0.00}");

        double ratingSum = books.Aggregate(
            0.0,
            (sum, book) => sum + book.Rating
        );

        Console.WriteLine($"Сума рейтингів: {ratingSum:0.00}");
    }
}
