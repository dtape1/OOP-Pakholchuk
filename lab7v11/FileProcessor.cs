using System;
using System.Collections.Generic;
using System.IO;

public class FileProcessor
{
    private int _failCount = 0;

    public List<string> LoadProductNames(string path)
    {
        _failCount++;

        if (_failCount <= 2)
            throw new FileNotFoundException("Файл не знайдено (імітація).");

        return new List<string>
        {
            "Хліб",
            "Молоко",
            "Цукор",
            "Кава"
        };
    }
}