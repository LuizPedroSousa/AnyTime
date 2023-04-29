namespace AnyTime.Core.Domain.Shared;
public class NotFoundException : BaseException
{
  public NotFoundException(string message) : base(message, StatusException.NotFound)
  {
  }
}