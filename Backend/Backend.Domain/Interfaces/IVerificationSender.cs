namespace Backend.Domain.Interfaces;

public interface IVerificationSender
{
    Task<bool> SendAsync(string email, string code, CancellationToken cancellationToken = default);
}