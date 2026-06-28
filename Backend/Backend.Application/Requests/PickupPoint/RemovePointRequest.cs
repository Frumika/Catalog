using Backend.Application.Requests.Base;

namespace Backend.Application.Requests.PickupPoint;

public class RemovePointRequest
{
    public string UserSessionId { get; set; } = string.Empty;
    public int PickupPointId { get; set; }

    public ValidationResult Validate()
    {
        bool isUserSessionIdValid = !string.IsNullOrWhiteSpace(UserSessionId);
        bool isPickupPointIdValid = PickupPointId > 0;

        bool isValid = isUserSessionIdValid && isPickupPointIdValid;

        if (!isValid)
        {
            List<string> errors = new();
            if (!isUserSessionIdValid) errors.Add("User Session Id mustn't be empty");
            if (!isPickupPointIdValid) errors.Add("Pickup Point Id must be greater than 0");
            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}