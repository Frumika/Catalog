using Backend.Application.Common.Base;

namespace Backend.Application.Services.Reviews.Requests;

public class GetListRequest : IValidatableRequest
{
    public int ProductId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public ValidationResult Validate()
    {
        bool isProductIdValid = ProductId > 0;
        bool isPageNumberValid = PageNumber > 0;
        bool isPageSizeValid = PageSize > 0;

        bool isRequestValid = isPageNumberValid && isPageSizeValid;
        if (!isRequestValid)
        {
            List<string> errors = new();
            
            if (!isProductIdValid) errors.Add("PageNumber must be greater than 0");
            if (!isPageNumberValid) errors.Add("PageNumber must be greater than 0");
            if (!isPageSizeValid) errors.Add("PageSize must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}