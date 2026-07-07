using Backend.Domain.Interfaces;
using Resend;


namespace Backend.Infrastructure.Services.Notifications;

public class ResendEmailSender : IVerificationSender
{
    private readonly IResend _resend;

    public ResendEmailSender(IResend resend)
    {
        _resend = resend;
    }

    public async Task<bool> SendAsync(string email, string code, CancellationToken cancellationToken = default)
    {
        var message = new EmailMessage
        {
            From = "onboarding@resend.dev",
            To = email,
            Subject = "Код подтверждения для входа",
            HtmlBody = $"<h3>Ваш код доступа: <strong>{code}</strong></h3><p>Код действителен 5 минут.</p>"
        };

        try
        {
            var response = await _resend.EmailSendAsync(message, cancellationToken);
            return response.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}