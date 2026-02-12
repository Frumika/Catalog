using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Review;

public class LeaveRequest : IValidatableRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Score { get; set; }
    public string? Text { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        bool isProductIdValid = ProductId > 0;
        bool isScoreValid = Score is >= 1 and <= 5;
        bool isTextValid = Text is null || !string.IsNullOrWhiteSpace(Text);

        bool isRequestValid = isUserSessionIdValid && isProductIdValid && isScoreValid && isTextValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isUserSessionIdValid) errors.Add("UserSessionId mustn't be empty");
            if (!isProductIdValid) errors.Add("ProductId must be greater than 0");
            if (!isScoreValid) errors.Add("Score must be between 1 and 5");
            if(!isTextValid) errors.Add("Text must be null or have data");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}