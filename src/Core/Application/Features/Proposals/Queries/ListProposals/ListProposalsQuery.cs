using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Queries.ListProposals;

using AnyTime.Core.Domain.Modules.Proposals;

public class ListProposalsQuery : IRequest<IReadOnlyList<Proposal>>
{

}