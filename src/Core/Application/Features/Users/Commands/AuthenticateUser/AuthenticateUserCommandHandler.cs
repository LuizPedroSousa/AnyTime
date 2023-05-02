using MediatR;

namespace AnyTime.Core.Application.Features.Users.Commands.AuthenticateUser;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;
using AnyTime.Core.Domain.Shared;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Either<BaseException, SignInResult>>
{
  private readonly AuthenticationProvider _authenticationProvider;
  public AuthenticateUserCommandHandler(AuthenticationProvider authenticationProvider)
  {
    _authenticationProvider = authenticationProvider;
  }

  public async Task<Either<BaseException, SignInResult>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
  {
    var authenticatedOrError = await this._authenticationProvider.SignIn(request.dto);

    if (authenticatedOrError.IsLeft())
    {
      return authenticatedOrError.left;
    }

    return authenticatedOrError.right;
  }
}