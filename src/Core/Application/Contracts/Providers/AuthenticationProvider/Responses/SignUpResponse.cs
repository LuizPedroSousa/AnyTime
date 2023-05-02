using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;
public class SignUpResponse : Either<BaseException, User>
{
  public SignUpResponse(BaseException left) : base(left)
  {
  }

  public SignUpResponse(User right) : base(right)
  {
  }

  public static implicit operator SignUpResponse(InvalidException exception) => new SignUpResponse(exception);
  public static implicit operator SignUpResponse(User result) => new SignUpResponse(result);

}