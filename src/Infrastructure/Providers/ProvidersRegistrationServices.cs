using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AnyTime.Infrastructure.Providers;

using System.Reflection;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models.Settings;
using AnyTime.Core.Application.Contracts.Providers.HeadlessProvider;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.NLPProvider;
using AnyTime.Infrastructure.Persistence.DatabaseContext;
using AnyTime.Infrastructure.Providers.Authentication;
using AnyTime.Infrastructure.Providers.Authentication.Models;
using AnyTime.Infrastructure.Providers.Headless;
using AnyTime.Infrastructure.Providers.Markdown;
using AnyTime.Infrastructure.Providers.NLP;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

public static class ProvidersRegistrationServices
{
  public static IServiceCollection ConfigureProvidersServices(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

    services.AddScoped<HeadlessProvider, PuppeteerHeadlessProvider>();
    services.AddScoped<MarkdownProvider, MarkdigMarkdownProvider>();
    services.AddScoped<NLPProvider, GPTNLPProvider>();
    services.AddIdentity<UserModel, IdentityRole>(options =>
               {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = true;
                 options.Password.RequiredLength = 6;
                 options.User.RequireUniqueEmail = true;

                 // Lockout settings.
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 options.Lockout.MaxFailedAccessAttempts = 5;
                 options.Lockout.AllowedForNewUsers = true;
               })
               .AddEntityFrameworkStores<AuthenticationDatabaseContext>()
               .AddDefaultTokenProviders();

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidIssuer = configuration["JwtSettings:issuer"],
        ValidAudience = configuration["JwtSettings:audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]))

      };
    });

    services.AddTransient<AuthenticationProvider, IdentityAuthenticationProvider>();

    return services;
  }

}
