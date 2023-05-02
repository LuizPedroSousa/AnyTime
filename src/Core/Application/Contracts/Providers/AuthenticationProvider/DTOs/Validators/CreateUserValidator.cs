using FluentValidation;

namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators.Shared;

public class CreateUserValidator : AbstractValidator<CreateUserDTO>
{
  public CreateUserValidator()
  {
    Include(new BaseUserValidator());
    RuleFor(user => user.password_confirmation)
      .NotEmpty().WithMessage("The {PropertyName} is required")
      .Equal(user => user.password)
      .WithMessage("The password and {PropertyName} do not match.");
    RuleFor(user => user.first_name).NotEmpty().WithMessage("The {PropertyName} is required");
    RuleFor(user => user.last_name).NotEmpty().WithMessage("The {PropertyName} is required");
  }
}