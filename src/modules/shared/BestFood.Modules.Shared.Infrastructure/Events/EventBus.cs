using BestFood.Modules.Shared.Application.Events;
using BestFood.Modules.Shared.Domain.Events;
using MassTransit;

namespace BestFood.Modules.Shared.Infrastructure.Events;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
        where T : IEvent => await bus.Publish(@event, cancellationToken);
}