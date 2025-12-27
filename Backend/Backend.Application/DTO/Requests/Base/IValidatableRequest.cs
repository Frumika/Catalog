namespace Backend.Application.DTO.Requests.Base;

public interface IValidatableRequest
{
    ValidationResult Validate();
}