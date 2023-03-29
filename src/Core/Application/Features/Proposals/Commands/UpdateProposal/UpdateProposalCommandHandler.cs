using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Responses;
using AutoMapper;
using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal;

public class UpdateProposalCommandHandler : IRequestHandler<UpdateProposalCommand, UpdateProposalResponse>
{
  private readonly ProposalsRepository _proposalsRepository;

  private readonly IMapper _mapper;

  public UpdateProposalCommandHandler(ProposalsRepository proposalsRepository, IMapper mapper)
  {
    _proposalsRepository = proposalsRepository;
    _mapper = mapper;
  }

  public async Task<UpdateProposalResponse> Handle(UpdateProposalCommand request, CancellationToken cancellationToken)
  {
    var proposalExsts = await _proposalsRepository.GetById(request.id);

    if (proposalExsts.IsLeft())
    {
      return proposalExsts.left;
    }

    var updatedProposal = _mapper.Map(request, proposalExsts.right);

    await _proposalsRepository.Update(updatedProposal);

    return updatedProposal;
  }
}
