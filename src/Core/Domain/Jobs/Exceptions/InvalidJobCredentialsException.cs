using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Domain.Jobs.Exceptions;
public class InvalidJobCredentialsException : BaseException
{
  public InvalidJobCredentialsException(string message) : base(message, StatusException.BadRequest)
  {
  }
}
