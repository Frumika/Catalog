using System.Text.RegularExpressions;

namespace Catalog.Application.Logic;

public static class IdentityValidator
{
    private const string EmailRegexTemplate = @"^[A-Za-z0-9+_.-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,6}$";
    private const string LoginRegexTemplate = @"^[A-Za-z0-9_.!@#$%^&*(){}\-]+$";
    private const string PasswordRegexTemplate = @"^[A-Za-z0-9_.!@#$%^&*(){}\-]+$";

    private static readonly Regex EmailRegex = new(EmailRegexTemplate, RegexOptions.Compiled);
    private static readonly Regex LoginRegex = new(LoginRegexTemplate, RegexOptions.Compiled);
    private static readonly Regex PasswordRegex = new(PasswordRegexTemplate, RegexOptions.Compiled);

    
    public static bool IsValidEmail(string email) =>
        !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);

    public static bool IsValidLogin(string login) =>
        !string.IsNullOrWhiteSpace(login) && LoginRegex.IsMatch(login);

    public static bool IsValidPassword(string password) =>
        !string.IsNullOrWhiteSpace(password) && PasswordRegex.IsMatch(password);
}