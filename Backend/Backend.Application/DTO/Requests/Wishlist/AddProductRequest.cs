using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Wishlist;

public class AddProductRequest: IValidatableRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int ProductId { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        bool isProductIdValid = ProductId > 0;

        bool isRequestValid = isUserSessionIdValid && isProductIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isUserSessionIdValid) errors.Add("User Session Id mustn't be empty");
            if (!isProductIdValid) errors.Add("Product Id must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}