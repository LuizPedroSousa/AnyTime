using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider;
using AnyTime.Core.Application.Features.Users.Commands.CreateUser;
using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Core.Domain.Shared;
using MediatR;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Either<BaseException, User>>
{
  private readonly AuthenticationProvider _authenticationProvider;
  public CreateUserCommandHandler(AuthenticationProvider authenticationProvider)
  {
    _authenticationProvider = authenticationProvider;
  }

  public async Task<Either<BaseException, User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
  {
    var userOrError = await _authenticationProvider.SignUp(request.dto);

    if (userOrError.IsLeft())
    {
      return userOrError.left;
    }

    return userOrError.right;
  }
}