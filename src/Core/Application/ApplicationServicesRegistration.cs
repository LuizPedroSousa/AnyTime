using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTime.Core.Application;

public static class ApplicationServicesRegistration
{
  public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(Assembly.GetExecutingAssembly());

    return services;
  }
}
