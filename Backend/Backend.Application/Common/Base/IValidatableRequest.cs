namespace Backend.Application.Common.Base;

public interface IValidatableRequest
{
    ValidationResult Validate();
}