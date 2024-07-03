using BestFood.Modules.Shared.Application.Time;

namespace BestFood.Modules.Shared.Infrastructure.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}