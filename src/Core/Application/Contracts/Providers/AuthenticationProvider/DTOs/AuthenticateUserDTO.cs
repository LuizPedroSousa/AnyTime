namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Shared;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models;


public class AuthenticateUserDTO : BaseUserDTO
{
  public Token token { get; set; }
}