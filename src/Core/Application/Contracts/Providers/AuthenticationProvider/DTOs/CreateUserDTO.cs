namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Shared;

public class CreateUserDTO : BaseUserDTO
{
  public string first_name { get; set; }
  public string last_name { get; set; }

  public string username { get; set; }

  public string password_confirmation { get; set; }
}