using SharedKernel.Shared;

namespace Core.Common;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}