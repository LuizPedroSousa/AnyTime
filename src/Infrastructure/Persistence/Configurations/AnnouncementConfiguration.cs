using System.IO.Pipes;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Configurations;
using AnyTime.Core.Domain.Modules.Jobs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
  public void Configure(EntityTypeBuilder<Announcement> builder)
  {
    builder.HasIndex(announcement => announcement.url).IsUnique();
    builder.Property(announcement => announcement.tags)
    .HasConversion(tag => string.Join(",", tag),
                   tagString => tagString.Split(",", StringSplitOptions.RemoveEmptyEntries)
                   .ToList());
    builder.HasOne<Author>(announcement => announcement.author);
  }
}
