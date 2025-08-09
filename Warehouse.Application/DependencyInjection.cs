using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Warehouse.Application.Mappings;

namespace Warehouse.Application;

public static class DependencyApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        
        return services;
    }
}