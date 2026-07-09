using Backend.Application.Common.Base;

namespace Backend.Application.Services.Carts.Requests;

public class RemoveProductRequest : IValidatableRequest
{
    public int ProductId { get; set; }

    public RemoveProductRequest()
    {
    }

    public RemoveProductRequest(UpdateProductQuantityRequest request)
    {
        ProductId = request.ProductId;
    }

    public ValidationResult Validate()
    {
        bool isProductIdValid = ProductId > 0;

        bool isRequestValid = isProductIdValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isProductIdValid) errors.Add("Product Id must be greater than 0");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}