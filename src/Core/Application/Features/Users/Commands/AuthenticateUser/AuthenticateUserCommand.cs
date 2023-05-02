using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;
using AnyTime.Core.Domain.Shared;
using MediatR;

namespace AnyTime.Core.Application.Features.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<Either<BaseException, SignInResult>>
{
  public AuthenticateUserDTO dto { get; set; }
}