using Backend.Application.Common.Base;

namespace Backend.Application.Services.Reviews.Requests;

public class DeleteRequest : IValidatableRequest
{
    public int ReviewId { get; set; }

    public ValidationResult Validate()
    {
        bool isReviewIdValid = ReviewId > 0;

        bool isRequestValid = isReviewIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();
            if (!isReviewIdValid) errors.Add("ReviewId must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}