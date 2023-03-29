using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Responses;
using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal;

public class UpdateProposalCommand : IRequest<UpdateProposalResponse>
{
  public string id { get; set; }
  public string title { get; set; }
  public string description { get; set; }
}