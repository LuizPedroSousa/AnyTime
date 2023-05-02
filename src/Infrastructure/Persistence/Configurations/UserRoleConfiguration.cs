using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
  public void Configure(EntityTypeBuilder<IdentityRole> builder)
  {
    builder.HasData(new[] {
      new IdentityRole
      {
        Id = Guid.NewGuid().ToString(),
        Name = "freelancer",
        NormalizedName = "FREELANCER"
      },
      new IdentityRole {
        Id = Guid.NewGuid().ToString(),
        Name = "author",
        NormalizedName = "AUTHOR"
      },
    });
  }
}
