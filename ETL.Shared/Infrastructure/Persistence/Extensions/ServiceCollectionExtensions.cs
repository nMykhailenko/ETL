using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ETL.Shared.Infrastructure.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration, 
        bool migrate = true)
    {
        var connectionString = configuration.GetConnectionString("AppDb");
        services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

        if (migrate)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        return services;
    }
}