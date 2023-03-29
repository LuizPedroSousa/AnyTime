using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Configurations;

using AnyTime.Core.Domain.Modules.Jobs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
  public void Configure(EntityTypeBuilder<Job> builder)
  {
  }
}