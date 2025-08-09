using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.Domain.Currency.Interfaces;
using Warehouse.Infrastructure.Repositories;

namespace Warehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IBalanceRepository, BalanceRepository>();
        services.AddScoped<IInboundDocumentRepository, InboundDocumentRepository>();
        services.AddScoped<IInboundResourceRepository, InboundResourceRepository>();
        services.AddScoped<IOutboundResourceRepository, OutboundResourceRepository>();
        services.AddScoped<IOutboundDocumentRepository, OutboundDocumentRepository>();
        services.AddSingleton<IDbConnectionFactory, SqliteDbConnectionFactory>();
        
        
        return services;
    }
}