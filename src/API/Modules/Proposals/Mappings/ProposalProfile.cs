using AutoMapper;

namespace AnyTime.API.Modules.Proposals.Mappings;

using AnyTime.API.Modules.Proposals.Protos;
using AnyTime.Core.Application.Features.Proposals.Commands.UpdateProposal;
using AnyTime.Core.Domain.Modules.Jobs;

public class ProposalProfile : Profile
{
  public ProposalProfile()
  {
    CreateMap<Proposal, ProposalReply>();
    CreateMap<UpdateProposalRequest, UpdateProposalCommand>();
  }
}