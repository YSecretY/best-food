using BestFood.Modules.Shared.Domain.Events;

namespace BestFood.Modules.Shared.Application.Events;

public interface IEventBus
{
    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        where T : IEvent;
}