namespace BestFood.Modules.Users.Application.Services.Password;

public interface IPasswordService
{
    public string HashPassword(string password);

    public bool Verify(string password, string passwordHash);
}