using AutoMapper;
using Grpc.Core;
using MediatR;

namespace AnyTime.API.Modules.Proposals.Services;

using AnyTime.API.Modules.Announcements.Protos;
using AnyTime.API.Modules.Proposals.Protos;
using AnyTime.API.Shared.Mappings;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Responses;
using AnyTime.Core.Application.Features.Proposals.Queries.ListProposals;
using AnyTime.Core.Domain.Modules.Jobs;

public class ProposalService : ProposalContract.ProposalContractBase
{

  private readonly ILogger<ProposalService> _logger;
  private readonly IMediator _mediator;
  private readonly IMapper _mapper;

  public ProposalService(ILogger<ProposalService> logger, IMediator mediator, IMapper mapper)
  {
    _logger = logger;
    _mediator = mediator;
    _mapper = mapper;
  }


  public override async Task List(ListProposalRequest request, IServerStreamWriter<ProposalReply> responseStream, ServerCallContext context)
  {

    var proposals = await this._mediator.Send<IReadOnlyList<Proposal>>(new ListProposalsQuery());

    foreach (var proposal in proposals)
    {
      await responseStream.WriteAsync(_mapper.Map<ProposalReply>(proposal));
    }
  }

  public override async Task<ProposalReply> Update(UpdateProposalRequest request, ServerCallContext context)
  {
    var proposalOrError = await _mediator.Send<UpdateProposalResponse>(_mapper.Map<UpdateProposalCommand>(request));

    if (proposalOrError.IsLeft())
    {
      throw RpcMapping.MapException(proposalOrError.left);
    }

    return _mapper.Map<ProposalReply>(proposalOrError.right);
  }
}