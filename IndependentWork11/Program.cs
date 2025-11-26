using System;
using System.Net.Http;
using System.Threading;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;

// Самостійна робота №11
// Тема: Кейси Polly/Retry. Короткий звіт.
// У файлі нижче для кожного сценарію є короткий опис, вибір політики та код.

internal class Program
{
    private static int _apiAttempts = 0;
    private static int _dbAttempts = 0;
    private static int _reportAttempts = 0;

    static void Main(string[] args)
    {
        Console.WriteLine(" IndependentWork11 | Polly кейси \n");

        Scenario1_ApiWithRetry();
        Scenario2_DatabaseWithRetryAndBreaker();
        Scenario3_LongOperationWithTimeoutAndFallback();

        Console.WriteLine("\n Кінець роботи ");
    }

    // -------------------------------------------------------
    // СЦЕНАРІЙ 1: Виклик зовнішнього API з політикою Retry
    //
    // Проблема:
    //   Зовнішній API може тимчасово падати з помилкою HttpRequestException.
    //
    // Обрана політика Polly:
    //   Handle<HttpRequestException>() + WaitAndRetry з експоненційною затримкою.
    //
    // Очікувана поведінка:
    //   Перші два виклики падають, далі запит проходить,
    //   у логах видно спроби та затримку між ними.
    // -------------------------------------------------------
    private static void Scenario1_ApiWithRetry()
    {
        Console.WriteLine(" Сценарій 1: Зовнішній API + Retry ");

        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetry(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt - 1)),
                onRetry: (ex, delay, attempt, context) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Retry {attempt}, чекаємо {delay.TotalSeconds} c. Причина: {ex.Message}");
                });

        try
        {
            string result = retryPolicy.Execute(() => CallExternalApi("https://api.example.com/products"));
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Результат API: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] API не вдалося викликати після всіх спроб: {ex.Message}");
        }

        Console.WriteLine();
    }

    private static string CallExternalApi(string url)
    {
        _apiAttempts++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Спроба {_apiAttempts}: виклик API {url}");

        // Перші 2 спроби "ламаємо"
        if (_apiAttempts <= 2)
        {
            throw new HttpRequestException("Тимчасова помилка API (імітація).");
        }

        return "Дані з API";
    }

    // -------------------------------------------------------
    // СЦЕНАРІЙ 2: Умова з “базою даних” + Retry + CircuitBreaker
    //
    // Проблема:
    //   Підключення до БД може тимчасово падати, але якщо помилки тривають
    //   занадто довго – краще на деякий час не чіпати ресурс.
    //
    // Обрана політика:
    //   Retry (кілька разів) + CircuitBreaker (якщо помилки не зникають).
    //
    // Очікувана поведінка:
    //   Кілька перших запитів кидають помилки.
    //   Якщо їх забагато підряд – брейкер “ламається”, і наступні виклики
    //   одразу відхиляються без спроб.
    // -------------------------------------------------------
    private static void Scenario2_DatabaseWithRetryAndBreaker()
    {
        Console.WriteLine(" Сценарій 2: Умова з БД + Retry + CircuitBreaker ");

        var retryPolicy = Policy
            .Handle<Exception>()
            .Retry(
                retryCount: 2,
                onRetry: (ex, attempt) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] DB retry {attempt}. Причина: {ex.Message}");
                });

        var breakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreaker(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromSeconds(5),
                onBreak: (ex, timespan) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit BREAKER: пауза {timespan.TotalSeconds} c. Причина: {ex.Message}");
                },
                onReset: () =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Circuit BREAKER: reset, можна пробувати ще раз.");
                });

        var dbPolicy = Policy.Wrap(retryPolicy, breakerPolicy);

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"\nСпроба звернення до БД #{i}");

            try
            {
                string data = dbPolicy.Execute(ReadFromDatabase);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Дані з БД: {data}");
            }
            catch (BrokenCircuitException)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Брейкер відкритий, запит до БД не виконується.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Помилка при роботі з БД: {ex.Message}");
            }

            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }

    private static string ReadFromDatabase()
    {
        _dbAttempts++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Спроба підключення до БД #{_dbAttempts}");

        // Перші 3 спроби імітуємо як “погане підключення”
        if (_dbAttempts <= 3)
        {
            throw new Exception("Помилка з'єднання з БД (імітація).");
        }

        return "Рядок з бази даних";
    }

    // -------------------------------------------------------
    // СЦЕНАРІЙ 3: Довга операція + Timeout + Fallback
    //
    // Проблема:
    //   Деякі операції можуть виконуватись занадто довго і “підвисати”.
    //
    // Обрана політика:
    //   Timeout (обриває занадто довгі операції) + Fallback, який повертає
    //   резервне значення, якщо сталась Timeout-помилка.
    //
    // Очікувана поведінка:
    //   Операція спочатку не встигає вкластися в допустимий час,
    //   спрацьовує fallback і в логах видно, що повертається резервний результат.
    // -------------------------------------------------------
    private static void Scenario3_LongOperationWithTimeoutAndFallback()
    {
        Console.WriteLine(" Сценарій 3: Довга операція + Timeout + Fallback ");

        var timeoutPolicy = Policy
            .Timeout(
                TimeSpan.FromSeconds(2),
                TimeoutStrategy.Pessimistic,
                onTimeout: (context, timespan, task, ex) =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout після {timespan.TotalSeconds} c.");
                });

        var fallbackPolicy = Policy<string>
            .Handle<TimeoutRejectedException>()
            .Fallback(
                fallbackAction: cancellationToken =>
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Спрацював fallback, повертаємо резервний результат.");
                    return "Резервний звіт";
                });

        var reportPolicy = fallbackPolicy.Wrap(timeoutPolicy);

        string report = reportPolicy.Execute(GenerateReport);

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Результат операції: {report}");
        Console.WriteLine();
    }

    private static string GenerateReport()
    {
        _reportAttempts++;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Запуск формування звіту. Спроба #{_reportAttempts}");

        // Імітуємо довгу операцію
        Thread.Sleep(5000);

        return "Готовий звіт";
    }
}
