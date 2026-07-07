namespace Backend.Domain.Interfaces;

public interface ICodeStorage
{
    Task SaveCodeAsync(string email, string code);
    Task<string?> GetCodeAsync(string email);
    Task RemoveCodeAsync(string email);
}