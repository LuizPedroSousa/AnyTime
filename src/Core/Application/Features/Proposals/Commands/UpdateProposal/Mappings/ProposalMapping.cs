using AnyTime.Core.Domain.Modules.Proposals;
using AutoMapper;

namespace AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal.Mappings;

public class ProposalProfile : Profile
{
  public ProposalProfile()
  {
    CreateMap<Proposal, UpdateProposalCommand>();
    CreateMap<UpdateProposalCommand, Proposal>();
  }
}
