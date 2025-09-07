using Accounts.Application;
using Accounts.Infrastructure;
using Framework.Models;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAccountsManagementModule(configuration)
            .AddFramework();
        
        return services;
    }
    
    private static IServiceCollection AddFramework(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<UserScopedData>();

        return services;
    }
    
    private static IServiceCollection AddAccountsManagementModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAccountsApplication()
            .AddAccountsInfrastructure(configuration);
        
        return services;
    }
}