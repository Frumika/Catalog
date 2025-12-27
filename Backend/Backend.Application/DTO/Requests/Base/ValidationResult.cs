namespace Backend.Application.DTO.Requests.Base;

public class ValidationResult
{
    public bool IsValid { get; private set; }
    public string? Message { get; private set; }

    public static ValidationResult Success(string? message = null)
    {
        return new ValidationResult
        {
            IsValid = true,
            Message = message
        };
    }

    public static ValidationResult Fail(string? message = null)
    {
        return new ValidationResult
        {
            IsValid = false,
            Message = message
        };
    }
}