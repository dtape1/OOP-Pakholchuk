using System;
using System.Threading;

public static class RetryHelper
{
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool> shouldRetry = null)
    {
        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        for (int attempt = 1; attempt <= retryCount; attempt++)
        {
            try
            {
                Console.WriteLine($"[LOG] Спроба {attempt}...");
                return operation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LOG] Помилка: {ex.Message}");

                if (attempt == retryCount || (shouldRetry != null && !shouldRetry(ex)))
                    throw;

                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));
                Console.WriteLine($"[LOG] Очікування {delay.TotalMilliseconds} мс перед повтором...");
                Thread.Sleep(delay);
            }
        }

        throw new Exception("Непередбачена помилка.");
    }
}