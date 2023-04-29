using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTime.Infrastructure;

using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Infrastructure.Persistence;
using AnyTime.Infrastructure.Providers.Headless;
using AnyTime.Infrastructure.Providers.Markdown;
using AnyTime.Infrastructure.Providers.NLP;

public static class InfrastructureRegistrationServices
{
  public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<HeadlessProvider, PuppeteerHeadlessProvider>();
    services.AddScoped<MarkdownProvider, MarkdigMarkdownProvider>();
    services.AddScoped<NLPProvider, GPTNLPProvider>();
    services.ConfigurePersistenceServices(configuration);
    return services;
  }
}