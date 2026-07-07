namespace Backend.Domain.Interfaces;

public interface ICodeStorage
{
    Task SaveCodeAsync(string email, string code, TimeSpan expiration);
    Task<string?> GetCodeAsync(string email);
    Task RemoveCodeAsync(string email);
}