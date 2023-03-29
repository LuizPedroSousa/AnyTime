namespace AnyTime.Core.Domain.Shared;

public enum StatusException
{
  BadRequest,
  NotFound
}

public class BaseException
{
  public string message { get; set; }
  public StatusException status { get; set; }

  public BaseException(string message, StatusException status)
  {
    this.message = message;
    this.status = status;
  }
}