using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.DatabaseContext;

using System.Threading;
using System.Threading.Tasks;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Modules.Proposals;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Persistence.Configurations;

public class PersistenceDatabaseContext : DbContext
{
  public DbSet<Job> jobs { get; set; }
  public DbSet<Author> authors { get; set; }
  public DbSet<Announcement> announcements { get; set; }
  public DbSet<Proposal> proposals { get; set; }
  public DbSet<Platform> platforms { get; set; }

  public PersistenceDatabaseContext(DbContextOptions<PersistenceDatabaseContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {

    builder.ApplyConfiguration(new AnnouncementConfiguration());
    builder.ApplyConfiguration(new AuthorConfiguration());
    builder.ApplyConfiguration(new JobConfiguration());
    builder.ApplyConfiguration(new PlatformConfiguration());
    builder.ApplyConfiguration(new ProposalConfiguration());
    base.OnModelCreating(builder);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var entries = from entity in base.ChangeTracker.Entries<BaseEntity>()
                  where entity.State == EntityState.Added || entity.State == EntityState.Modified
                  select entity;

    foreach (var entry in entries)
    {
      entry.Entity.updatedAt = DateTime.UtcNow;

      if (entry.State == EntityState.Added)
      {
        entry.Entity.createdAt = DateTime.UtcNow;
      }

    }

    return base.SaveChangesAsync(cancellationToken);
  }

}