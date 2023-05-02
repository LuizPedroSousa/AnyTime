using AnyTime.Infrastructure.Persistence;

namespace AnyTime.Infrastructure;

using AnyTime.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureRegistrationServices
{
  public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.ConfigurePersistenceServices(configuration);
    services.ConfigureProvidersServices(configuration);

    return services;
  }
}