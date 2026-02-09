IReviewValidator validator = new ReviewValidator();
IReviewRepository repository = new ReviewRepository();
IRatingCalculator calculator = new RatingCalculator();

var service = new ProductReviewService(validator, repository, calculator);
service.AddReview("Хороший товар", 5);
