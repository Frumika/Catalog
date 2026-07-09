using Backend.Application.Common.Base;

namespace Backend.Application.Services.Orders.Requests;

public class MakeOrderRequest : IValidatableRequest
{
    public string RefreshToken { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(RefreshToken);
        if (!isUserSessionIdValid)
        {
            List<string> errors = new();
            errors.Add("RefreshToken mustn't be empty");
            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}