public interface IReviewValidator
{
    void Validate(string text, int rating);
}

public interface IReviewRepository
{
    void Save(string text, int rating);
}

public interface IRatingCalculator
{
    double Calculate(int rating);
}
