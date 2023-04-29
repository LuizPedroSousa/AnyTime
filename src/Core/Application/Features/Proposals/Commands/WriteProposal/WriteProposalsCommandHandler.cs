using MediatR;

namespace AnyTime.Core.Application.Features.Proposals.Commands.WriteProposals;

using System.Threading;
using System.Threading.Tasks;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider;
using AnyTime.Core.Application.Contracts.Providers.MarkdownProvider.Models;
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;

public class CreateProposalsCommandHandler : IRequestHandler<WriteProposalsCommand, Unit>
{

  private readonly MarkdownProvider _markdownProvider;

  private readonly JobsRepository _jobsRepository;

  public CreateProposalsCommandHandler(MarkdownProvider markdownProvider, JobsRepository jobsRepository)
  {
    _markdownProvider = markdownProvider;
    _jobsRepository = jobsRepository;
  }

  public async Task<Unit> Handle(WriteProposalsCommand request, CancellationToken cancellationToken)
  {
    var plans = await this._markdownProvider.ParseToString("proposals/templates/Plans.md");

    var jobs = await _jobsRepository.GetAllByStatus(request.status);

    foreach (Job job in jobs)
    {
      await this._markdownProvider.Write(new WriteModel
      {
        folder = $"proposals/{job.proposal.announcement.platform.ToString()}/{job.status}/",
        filename = $"{job.id}.md",
        content = @$"
## Announcement
### Title
{job.proposal.announcement.title}

### Description
<br/>
{job.proposal.announcement.description}

### Link
<br/>

{job.proposal.announcement.url}

# Proposal {job?.proposal?.id}

## Description
{job?.proposal?.description}
{plans}"
      });
    }

    return new Unit();
  }
}