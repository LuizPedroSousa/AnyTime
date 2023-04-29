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

    builder.Property(announcement => announcement.platform)
    .HasConversion(platform => MapPlaformEnumToString(platform),
    persistencePlatform => MapPlatformStringToEnum(persistencePlatform)
                   );

    builder.HasOne<Author>(announcement => announcement.author);
  }

  private string MapPlaformEnumToString(AnnouncementPlatform platform)
  {
    return platform switch
    {
      AnnouncementPlatform.FreelancerBR => "freelancer.br",
      AnnouncementPlatform.NineNineFreelas => "99freelas"
    };
  }

  private AnnouncementPlatform MapPlatformStringToEnum(string platform)
  {
    return platform switch
    {
      "freelancer.br" => AnnouncementPlatform.FreelancerBR,
      "99freelas" => AnnouncementPlatform.NineNineFreelas
    };
  }
}
