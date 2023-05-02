using AnyTime.Infrastructure.Persistence.Configurations;
using AnyTime.Infrastructure.Providers.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AnyTime.Infrastructure.Persistence.DatabaseContext;

public class AuthenticationDatabaseContext : IdentityDbContext<UserModel>
{
  public AuthenticationDatabaseContext(DbContextOptions<AuthenticationDatabaseContext> options)
      : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfiguration(new UserRoleConfiguration());
    base.OnModelCreating(builder);
  }
}
