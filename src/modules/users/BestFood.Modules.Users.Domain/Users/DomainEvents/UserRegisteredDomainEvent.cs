using BestFood.Modules.Shared.Domain.Events;

namespace BestFood.Modules.Users.Domain.Users.DomainEvents;

public sealed class UserRegisteredDomainEvent(string email, string nickname, UserRole role, DateTime occuredAt)
    : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Email { get; set; } = email;

    public string Nickname { get; set; } = nickname;

    public UserRole Role { get; set; } = role;

    public DateTime OccuredAt { get; set; } = occuredAt;
}