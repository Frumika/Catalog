using Backend.Application.Common.Base;

namespace Backend.Application.Services.Carts.Requests;

public class UpdateProductQuantityRequest : IValidatableRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public ValidationResult Validate()
    {
        bool isProductIdValid = ProductId > 0;
        bool isQuantityValid = Quantity >= 0;

        bool isRequestValid = isProductIdValid && isQuantityValid;
        if (!isRequestValid)
        {
            List<string> errors = new();
            
            if (!isProductIdValid) errors.Add("Product Id must be greater than 0");
            if (!isQuantityValid) errors.Add("Quantity must be greater than or equal to 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}