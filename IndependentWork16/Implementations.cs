public class ReviewValidator : IReviewValidator
{
    public void Validate(string text, int rating)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Невірний рейтинг");
    }
}

public class ReviewRepository : IReviewRepository
{
    public void Save(string text, int rating)
    {
        Console.WriteLine("Відгук збережено");
    }
}

public class RatingCalculator : IRatingCalculator
{
    public double Calculate(int rating)
    {
        return (rating + 4.0) / 2;
    }
}
