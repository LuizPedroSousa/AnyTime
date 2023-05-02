namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models;
using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Core.Domain.Shared;

public class SignInResult
{
  public User user { get; set; }
  public Token token { get; set; }
}

public class SignInResponse : Either<BaseException, SignInResult>
{
  public SignInResponse(BaseException left) : base(left)
  {
  }

  public SignInResponse(SignInResult right) : base(right)
  {
  }

  public static implicit operator SignInResponse(NotFoundException exception) => new SignInResponse(exception);
  public static implicit operator SignInResponse(InvalidException exception) => new SignInResponse(exception);
  public static implicit operator SignInResponse(SignInResult result) => new SignInResponse(result);
}