
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Configurations;
using AnyTime.Core.Domain.Modules.Jobs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
  public void Configure(EntityTypeBuilder<Author> builder)
  {
  }
}
