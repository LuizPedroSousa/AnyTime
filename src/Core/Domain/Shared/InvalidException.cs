namespace AnyTime.Core.Domain.Shared;
public class InvalidException : BaseException
{
  public InvalidException(string message) : base(message, StatusException.BadRequest)
  {
  }
}