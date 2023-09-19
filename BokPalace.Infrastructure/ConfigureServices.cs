using BokPalace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BokPalace.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlite("DataSource=BokPalace.db");
        });

        services.AddScoped<ApplicationDbContextInitializer>();

        return services;
    }


}
