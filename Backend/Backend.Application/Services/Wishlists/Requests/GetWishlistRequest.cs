using Backend.Application.Common.Base;


namespace Backend.Application.Services.Wishlists.Requests;

public class GetWishlistRequest : IValidatableRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public ValidationResult Validate()
    {
        bool isPageNumberValid = PageNumber > 0;
        bool isPageSizeValid = PageSize > 0;

        bool isRequestValid = isPageNumberValid && isPageSizeValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isPageNumberValid) errors.Add("Page number must be greater than 0");
            if (!isPageSizeValid) errors.Add("Page size must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}