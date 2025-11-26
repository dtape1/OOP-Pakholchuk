using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Лабараторна №7 | Патерн Retry ");

        var file = new FileProcessor();
        var net = new NetworkClient();

        Func<Exception, bool> retryPolicy = ex =>
            ex is FileNotFoundException || ex is HttpRequestException;

        Console.WriteLine("\n Читання продуктів з файлу ");
        var fileProducts = RetryHelper.ExecuteWithRetry(
            () => file.LoadProductNames("products.txt"),
            retryCount: 4,
            initialDelay: TimeSpan.FromMilliseconds(500),
            shouldRetry: retryPolicy
        );

        Console.WriteLine("Успішно прочитано:");
        fileProducts.ForEach(p => Console.WriteLine($" - {p}"));

        Console.WriteLine("\n Отримання продуктів з API ");
        var apiProducts = RetryHelper.ExecuteWithRetry(
            () => net.GetProductsFromApi("https://fake.api/products"),
            retryCount: 5,
            initialDelay: TimeSpan.FromMilliseconds(400),
            shouldRetry: retryPolicy
        );

        Console.WriteLine("Успішно отримано:");
        apiProducts.ForEach(p => Console.WriteLine($" - {p}"));
    }
}