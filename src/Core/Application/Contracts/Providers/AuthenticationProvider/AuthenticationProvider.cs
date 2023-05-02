namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;

public interface AuthenticationProvider
{
  Task<SignInResponse> SignIn(AuthenticateUserDTO credentials);
  Task<SignUpResponse> SignUp(CreateUserDTO credentials);
}