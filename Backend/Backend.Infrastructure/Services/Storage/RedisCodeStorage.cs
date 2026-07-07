using Backend.Domain.Interfaces;

namespace Backend.Infrastructure.Services.Storage;

public class RedisCodeStorage : ICodeStorage
{
    public Task SaveCodeAsync(string email, string code, TimeSpan expiration)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetCodeAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCodeAsync(string email)
    {
        throw new NotImplementedException();
    }
}