namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models.Settings;

public class JwtSettings
{
  public string key { get; set; }
  public string issuer { get; set; }
  public string audience { get; set; }
  public double duration_in_minutes { get; set; }
}