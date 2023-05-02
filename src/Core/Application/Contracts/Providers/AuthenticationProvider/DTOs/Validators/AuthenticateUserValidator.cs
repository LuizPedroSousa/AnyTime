using FluentValidation;

namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators.Shared;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserDTO>
{
  public AuthenticateUserValidator()
  {
    Include(new BaseUserValidator());
  }
}