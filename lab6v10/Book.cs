namespace lab6v10;

// Проста модель книги
public class Book
{
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }
    public double Rating { get; }

    public Book(string title, string author, int year, double rating)
    {
        Title = title;
        Author = author;
        Year = year;
        Rating = rating;
    }

    public override string ToString()
        => $"{Title} ({Author}, {Year}) — рейтинг {Rating:0.0}";
}
