using System.ComponentModel.DataAnnotations;
using BestFood.Modules.Shared.Domain.SharedConstants;

namespace BestFood.Modules.Users.Domain.Users;

public sealed class User
{
    private User
    (
        string email,
        string passwordHash,
        string nickname,
        UserRole role,
        DateTime createdAt
    )
    {
        Email = email;
        PasswordHash = passwordHash;
        Nickname = nickname;
        Role = role;
        CreatedAt = createdAt;
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    [MaxLength(SharedConstants.Email.MaxLength)]
    public string Email { get; private set; }

    [MaxLength(SharedConstants.PasswordHash.MaxLength)]
    public string PasswordHash { get; private set; }

    [MaxLength(SharedConstants.Nickname.MaxLength)]
    public string Nickname { get; private set; }

    public UserRole Role { get; private set; }

    public DateTime CreatedAt { get; init; }

    public static User Create
    (
        string email,
        string passwordHash,
        string nickname,
        UserRole role,
        DateTime createdAt
    )
    {
        if (email.Length > SharedConstants.Email.MaxLength)
            throw new ValidationException("Email is too long.");

        if (passwordHash.Length > SharedConstants.PasswordHash.MaxLength)
            throw new ValidationException("Password is too long.");

        if (nickname.Length > SharedConstants.Nickname.MaxLength)
            throw new ValidationException("Nickname is too long.");

        return new User
        (
            email: email,
            passwordHash: passwordHash,
            nickname: nickname,
            role: role,
            createdAt: createdAt
        );
    }
}