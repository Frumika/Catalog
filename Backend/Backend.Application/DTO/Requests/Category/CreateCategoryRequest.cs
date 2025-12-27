using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Category;

public class CreateCategoryRequest : IValidatableRequest
{
    public string Name { get; set; } = string.Empty;

    public ValidationResult Validate()
    {
        bool isNameValid = !string.IsNullOrWhiteSpace(Name);
        return isNameValid ? ValidationResult.Success() : ValidationResult.Fail("Name mustn't be empty");
    }
}