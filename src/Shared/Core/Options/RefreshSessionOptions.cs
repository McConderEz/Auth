using SharedKernel.Shared.ValueObjects;

namespace Core.Options;

public class RefreshSessionOptions
{
    public static string REFRESH_SESSION = "RefreshSession";
    
    public int ExpiredDaysTime { get; set; }
}