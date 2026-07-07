using Backend.Application.Common.Base;

namespace Backend.Application.Services.Auth.Requests;

public class LogoutRequest : IValidatableRequest
{
    public string SessionId { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        string? message = null;
        bool isSessionIdValid = !string.IsNullOrWhiteSpace(SessionId);

        if (!isSessionIdValid) message += "Session id is required";

        return isSessionIdValid ? ValidationResult.Success() : ValidationResult.Fail(message);
    }
}