namespace AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Responses;

using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;

public class UpdateProposalResponse : Either<BaseException, Proposal>
{
  public UpdateProposalResponse(BaseException left) : base(left)
  {
  }

  public UpdateProposalResponse(Proposal right) : base(right)
  {
  }

  public static implicit operator UpdateProposalResponse(Proposal result) => new UpdateProposalResponse(result);
  public static implicit operator UpdateProposalResponse(BaseException exception) => new UpdateProposalResponse(exception);
}