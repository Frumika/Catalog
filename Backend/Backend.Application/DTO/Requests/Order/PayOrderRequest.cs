using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Order;

public class PayOrderRequest : IValidatableRequest
{
    public string UserSessionId { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        if (!isUserSessionIdValid)
        {
            List<string> errors = new();
            errors.Add("User Session Id mustn't be empty");
            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}