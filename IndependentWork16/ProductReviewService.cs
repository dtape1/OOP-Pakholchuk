public class ProductReviewService
{
    private readonly IReviewValidator _validator;
    private readonly IReviewRepository _repository;
    private readonly IRatingCalculator _calculator;

    public ProductReviewService(
        IReviewValidator validator,
        IReviewRepository repository,
        IRatingCalculator calculator)
    {
        _validator = validator;
        _repository = repository;
        _calculator = calculator;
    }

    public void AddReview(string text, int rating)
    {
        _validator.Validate(text, rating);
        _repository.Save(text, rating);
        var avg = _calculator.Calculate(rating);

        Console.WriteLine($"Середній рейтинг: {avg}");
    }
}
