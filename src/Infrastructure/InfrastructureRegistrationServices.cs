using AnyTime.Core.Application.Contracts.Providers;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Infrastructure.Providers.Headless;
using AnyTime.Infrastructure.Providers.Markdown;
using AnyTime.Infrastructure.Providers.NLP;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTime.Infrastructure;

public static class InfrastructureRegistrationServices
{
  public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
  {
    services.AddTransient<HeadlessProvider, PuppeteerHeadlessProvider>();
    services.AddScoped<MarkdownProvider, MarkdigMarkdownProvider>();
    services.AddScoped<NLPProvider, GPTNLPProvider>();
    return services;
  }
}