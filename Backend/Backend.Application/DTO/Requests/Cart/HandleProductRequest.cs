using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Cart;

public class HandleProductRequest : IValidatableRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string? CartStateId { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        bool isProductIdValid = ProductId > 0;
        bool isQuantityValid = Quantity >= 0;
        bool isCartStateIdValid = CartStateId is null || !string.IsNullOrWhiteSpace(CartStateId);

        bool isRequestValid = isUserSessionIdValid && isProductIdValid && isQuantityValid && isCartStateIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isUserSessionIdValid) errors.Add("User Session Id mustn't be empty");
            if (!isProductIdValid) errors.Add("Product Id must be greater than 0");
            if (!isQuantityValid) errors.Add("Quantity must be greater than or equal to 0");
            if (!isCartStateIdValid) errors.Add("Cart State Id mustn't be empty");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}