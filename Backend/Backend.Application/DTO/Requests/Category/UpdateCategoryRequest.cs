using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Category;

public class UpdateCategoryRequest : IValidatableRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isIdValid = Id > 0;
        bool isNameValid = !string.IsNullOrWhiteSpace(Name);

        bool isRequestValid = isIdValid && isNameValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isIdValid) errors.Add("Id must be greater than 0");
            if (!isNameValid) errors.Add("Name mustn't be empty");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}