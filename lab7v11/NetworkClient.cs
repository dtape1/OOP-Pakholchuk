using System;
using System.Collections.Generic;
using System.Net.Http;

public class NetworkClient
{
    private int _failCount = 0;

    public List<string> GetProductsFromApi(string url)
    {
        _failCount++;

        if (_failCount <= 3)
            throw new HttpRequestException("Помилка запиту до API (імітація).");

        return new List<string>
        {
            "Сир",
            "Шоколад",
            "Печиво",
            "Макарони"
        };
    }
}