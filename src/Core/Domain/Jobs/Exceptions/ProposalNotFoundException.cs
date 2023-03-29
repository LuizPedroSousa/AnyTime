namespace AnyTime.Core.Domain.Modules.Jobs.Exceptions;

using AnyTime.Core.Domain.Shared;

public class ProposalNotFoundException : BaseException
{
  public ProposalNotFoundException() : base("Proposals not found", StatusException.NotFound)
  {
  }
}