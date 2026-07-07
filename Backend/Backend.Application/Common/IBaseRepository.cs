namespace Backend.Application.Common;

public interface IBaseRepository
{
    Task<int?> GetUserIdAsync(string? sessionId);
    void Add<T>(T entity) where T : class;
    void Remove<T>(T entity) where T : class;
    Task CommitAsync();
}