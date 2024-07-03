namespace BestFood.Modules.Shared.Domain.Events;

public interface IEvent
{
    public Guid Id { get; }

    public DateTime OccuredAt { get; }
}