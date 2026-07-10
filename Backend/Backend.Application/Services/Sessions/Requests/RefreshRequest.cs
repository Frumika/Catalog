using Backend.Application.Common.Base;

namespace Backend.Application.Services.Sessions.Requests;

public class RefreshRequest : IValidatableRequest
{
    public string RefreshToken { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;
        bool isSessionIdValid = !string.IsNullOrWhiteSpace(RefreshToken);

        if (!isSessionIdValid) message += "RefreshToken is required";

        return isSessionIdValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}