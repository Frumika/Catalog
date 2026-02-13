using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Review;

public class DeleteRequest : IValidatableRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int ReviewId { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        bool isReviewIdValid = ReviewId > 0;

        bool isRequestValid = isUserSessionIdValid && isReviewIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isUserSessionIdValid) errors.Add("UserSessionId mustn't be empty");
            if (!isReviewIdValid) errors.Add("ReviewId must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}