using BestFood.Modules.Shared.Domain.Events;

namespace BestFood.Modules.Users.Domain.Users.DomainEvents;

public sealed class UserRegisteredDomainEvent(string email, string nickname, UserRole role, DateTime occuredAt) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Email = email;

    public string Nickname = nickname;

    public UserRole Role = role;

    public DateTime OccuredAt { get; } = occuredAt;
}