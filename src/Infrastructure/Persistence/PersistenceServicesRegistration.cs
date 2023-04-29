using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using AnyTime.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnyTime.Infrastructure.Persistence;

public static class PersistenceServicesRegistration
{

  public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<PersistenceDatabaseContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("PersistenceConnectionString"));
    });

    services.AddScoped(typeof(GenericRepository<>), typeof(EFCoreGenericRepository<>));
    services.AddScoped<ProposalsRepository, EFCoreProposalsRepository>();
    services.AddScoped<AnnouncementsRepository, EFCoreAnnouncementsRepository>();
    services.AddScoped<JobsRepository, EFCoreJobsRepository>();
    services.AddScoped<PlatformsRepository, EFCorePlatformsRepository>();

    return services;
  }
}