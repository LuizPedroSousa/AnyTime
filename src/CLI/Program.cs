using AnyTime.CLI.Commands;
using AnyTime.Core.Application;
using AnyTime.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder().ConfigureAppConfiguration((hostContext, config) =>
{
  config.SetBasePath(Directory.GetCurrentDirectory());
  config.AddJsonFile("appsettings.json");
  config.AddEnvironmentVariables();

  if (args != null)
  {
    config.AddCommandLine(args);
  }


}).ConfigureServices((hostContext, services) =>
{

  services.AddOptions();

  services.ConfigureApplicationServices();
  services.ConfigureInfrastructureServices(hostContext.Configuration);
});

return await builder.RunCommandLineApplicationAsync<AnyTimeCommand>(args);