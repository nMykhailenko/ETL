using ETL.Api.Application.Hashes.Contracts;
using ETL.Shared.Infrastructure.Persistence.Extensions;

namespace ETL.Api.Application.Hashes.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHashes(this IServiceCollection services)
    {
        services.AddTransient<IHashService, HashService>();
        
        return services;
    }
}