using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Configurations;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
  public void Configure(EntityTypeBuilder<Platform> builder)
  {
    builder.HasIndex(platform => platform.url).IsUnique();

    builder.HasData(new[] {
      new Platform
      {
        id = "162b482f-a5c9-4ce4-8921-bc0b39eb1fe9",
        name = "freelancerbr",
        createdAt = DateTime.UtcNow,
        icon_url = "https://icones.pro/wp-content/uploads/2021/03/symbole-de-plume-bleue.png",
        url = "https://freelancer.com.br",
        thumb_url = "https://freelancer.com.br/images/main-page/find-section-bg.jpg"
      },
      new Platform
      {
        id = "69f1f410-a320-417b-9d44-762ac5970810",
        name = "99freelas",
        createdAt = DateTime.UtcNow,
        icon_url = "https://play-lh.googleusercontent.com/yNBbWLb1_Te7UELDjx4OjKpkYcW79nklxeB-xJThgaN0hiXpIDs2hHt3P9XISi4fEjwH=w240-h480-rw",
        url = "https://www.99freelas.com.br",
        thumb_url = "https://s3-sa-east-1.amazonaws.com/projetos-artes/fullsize%2F2014%2F04%2F07%2F16%2FWDL-Logo-39095_16236_061350792_1651536830.jpg"
      },
    });
  }
}
