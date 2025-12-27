using System.Text;
using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.User;

public class RegisterRequest : IValidatableRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isLoginValid = !string.IsNullOrWhiteSpace(Login);
        bool isPasswordValid = !string.IsNullOrWhiteSpace(Password);

        bool isRequestValid = isLoginValid && isPasswordValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isLoginValid) errors.Add("Login mustn't be empty");
            if (!isPasswordValid) errors.Add("Password mustn't be empty");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}