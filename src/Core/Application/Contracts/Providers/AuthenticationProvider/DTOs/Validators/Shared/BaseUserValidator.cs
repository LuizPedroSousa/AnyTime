using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Shared;
using FluentValidation;

namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators.Shared;

public class BaseUserValidator : AbstractValidator<BaseUserDTO>
{
  public BaseUserValidator()
  {
    RuleFor(u => u.email)
      .NotEmpty().WithMessage("{PropertyName} is required")
      .EmailAddress().WithMessage("{PropertyName} must be a valid email address");
    RuleFor(user => user.password)
      .NotEmpty().WithMessage("{PropertyName} is required")
      .MinimumLength(8)
      .WithMessage("{PropertyName} must be greater than {MinLength}")
      .Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$")
      .WithMessage("{PropertyName} must have at least one special caracter");
  }
}