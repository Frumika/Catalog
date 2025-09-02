namespace Catalog.DataAccess.Models;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}