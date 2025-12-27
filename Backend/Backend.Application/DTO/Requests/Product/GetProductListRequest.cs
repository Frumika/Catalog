using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Product;

public class GetProductListRequest : IValidatableRequest
{
    public int? CategoryId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public ValidationResult Validate()
    {
        bool isCategoryIdValid = CategoryId is null or > 0;
        bool isPageNumberValid = PageNumber > 0;
        bool isPageSizeValid = PageSize > 0;

        bool isRequestValid = isPageNumberValid && isPageSizeValid && isCategoryIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isCategoryIdValid) errors.Add("Category id must be greater than 0 or null");
            if (!isPageNumberValid) errors.Add("Page number must be greater than 0");
            if (!isPageSizeValid) errors.Add("Page size must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}