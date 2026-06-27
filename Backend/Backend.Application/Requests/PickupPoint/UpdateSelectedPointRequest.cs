using Backend.Application.Requests.Base;

namespace Backend.Application.Requests.PickupPoint;

public class UpdateSelectedPointRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    
    // Todo: дописать проверку
    public int PickupPointId { get; set; }

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