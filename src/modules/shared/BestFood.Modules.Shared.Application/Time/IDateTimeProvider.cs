namespace BestFood.Modules.Shared.Application.Time;

public interface IDateTimeProvider
{
    public DateTime CurrentTime { get; }
}