using Backend.Application.Common.Base;

namespace Backend.Application.Services.PickupPoints.Requests;

public class RemovePointRequest
{
    public int PickupPointId { get; set; }

    public ValidationResult Validate()
    {
        bool isPickupPointIdValid = PickupPointId > 0;

        bool isValid = isPickupPointIdValid;

        if (!isValid)
        {
            List<string> errors = new();
            if (!isPickupPointIdValid) errors.Add("Pickup Point Id must be greater than 0");
            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}