namespace Backend.Application.Requests.Base;

public interface IValidatableRequest
{
    ValidationResult Validate();
}