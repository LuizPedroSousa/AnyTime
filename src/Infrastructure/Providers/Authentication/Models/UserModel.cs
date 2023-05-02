using Microsoft.AspNetCore.Identity;

namespace AnyTime.Infrastructure.Providers.Authentication.Models;

public class UserModel : IdentityUser
{
  public string first_name { get; set; }
  public string last_name { get; set; }
}