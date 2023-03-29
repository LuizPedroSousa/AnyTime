using System.Reflection;
using AnyTime.API.Modules.Proposals.Services;
using AnyTime.Core.Application;
using AnyTime.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682


builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Add services to the container.
builder.Services.AddGrpc();


var app = builder.Build();

Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "DEBUG");
Environment.SetEnvironmentVariable("GRPC_TRACE", "api,cares_resolver,cares_address_sorting");

// Configure the HTTP request pipeline.
app.MapGrpcService<ProposalService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
