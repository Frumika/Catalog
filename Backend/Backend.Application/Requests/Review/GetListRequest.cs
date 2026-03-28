using Backend.Application.Requests.Base;

namespace Backend.Application.Requests.Review;

public class GetListRequest : IValidatableRequest
{
    public string? UserSessionId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = UserSessionId is null || !string.IsNullOrWhiteSpace(UserSessionId);
        bool isProductIdValid = ProductId > 0;
        bool isPageNumberValid = PageNumber > 0;
        bool isPageSizeValid = PageSize > 0;

        bool isRequestValid = isPageNumberValid && isPageSizeValid && isUserSessionIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isUserSessionIdValid) errors.Add("UserSessionId mustn't be empty");
            if (!isProductIdValid) errors.Add("PageNumber must be greater than 0");
            if (!isPageNumberValid) errors.Add("PageNumber must be greater than 0");
            if (!isPageSizeValid) errors.Add("PageSize must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}