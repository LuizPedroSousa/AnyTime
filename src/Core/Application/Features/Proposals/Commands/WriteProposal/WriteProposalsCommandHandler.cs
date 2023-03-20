using MediatR;

namespace AnyTime.Application.Features.Proposals.Commands.WriteProposals;

using System.Threading;
using System.Threading.Tasks;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider.Models;
using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommandHandler : IRequestHandler<WriteProposalsCommand, Unit>
{

  private readonly MarkdownProvider _markdownProvider;

  public CreateProposalsCommandHandler(MarkdownProvider markdownProvider
  )
  {
    _markdownProvider = markdownProvider;
  }

  public async Task<Unit> Handle(WriteProposalsCommand request, CancellationToken cancellationToken)
  {
    foreach (Proposal proposal in request.proposals)
    {
      await this._markdownProvider.Write(new WriteModel
      {
        path = $"proposals/{proposal.id}-proposal.md",
        content = @$"
# Proposal {proposal.id}

* Description
{proposal.description}

## Announcement
### {proposal.announcement.title}

*Description*
{proposal.announcement.description}

*Link*
{proposal.announcement.url}

        "
      });
    }

    return new Unit();
  }
}