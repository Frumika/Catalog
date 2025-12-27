using Backend.Application.DTO.Requests.Base;


namespace Backend.Application.DTO.Requests.Product;

public class UpdateProductRequest : IValidatableRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Count { get; set; }
    public string? Description { get; set; }

    public ValidationResult Validate()
    {
        bool isIdValid = Id > 0;
        bool isNameValid = !string.IsNullOrWhiteSpace(Name);
        bool isPriceValid = Price >= 0;
        bool isCountValid = Count >= 0;
        bool isDescriptionValid = Description is null || !string.IsNullOrWhiteSpace(Description);
        
        bool isRequestValid = isIdValid && isNameValid && isPriceValid && isCountValid && isDescriptionValid;
        if (!isRequestValid)
        {
            List<string> errors = new();

            if (!isIdValid) errors.Add("Id must be greater than 0");
            if (!isNameValid) errors.Add("Name mustn't be empty");
            if (!isPriceValid) errors.Add("Price must be greater than or equal to 0");
            if (!isCountValid) errors.Add("Count must be greater than or equal to 0");
            if (!isDescriptionValid) errors.Add("Description cannot be empty or consist only of white-space characters.");

            return ValidationResult.Fail(string.Join(Environment.NewLine, errors));
        }

        return ValidationResult.Success();
    }
}