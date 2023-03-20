using AnyTime.CLI.Commands;
using AnyTime.Core.Application;
using AnyTime.Infrastructure;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
{
  services.ConfigureApplicationServices();
  services.ConfigureInfrastructureServices();
});

return await builder.RunCommandLineApplicationAsync<AnyTimeCommand>(args);