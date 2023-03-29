namespace AnyTime.Core.Domain.Modules.Jobs.Exceptions;

using AnyTime.Core.Domain.Shared;

public class JobNotFoundException : BaseException
{
  public JobNotFoundException() : base("Job not found", StatusException.NotFound)
  {
  }
}