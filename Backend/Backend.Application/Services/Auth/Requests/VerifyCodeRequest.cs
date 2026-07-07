using Backend.Application.Common;
using Backend.Application.Common.Base;


namespace Backend.Application.Services.Auth.Requests;

public class VerifyCodeRequest : IValidatableRequest
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isEmailValid = IdentityValidator.IsValidEmail(Email);
        bool isCodeValid = !string.IsNullOrWhiteSpace(Code) && Code.Length == 6;
        bool isValid = isEmailValid && isCodeValid;

        if (!isValid)
        {
            List<string> errors = new();

            if (!isEmailValid) errors.Add("Email is not valid");
            if (!isCodeValid) errors.Add("Code is not valid");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}