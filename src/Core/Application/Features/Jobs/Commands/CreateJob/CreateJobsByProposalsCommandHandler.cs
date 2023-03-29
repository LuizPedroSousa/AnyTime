
using AnyTime.Core.Application.Contracts.Repositories;
using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;

namespace AnyTime.Core.Application.Features.Jobs.Commands.CreateJob;

public class CreateJobsByProposalsCommandHandler : IRequestHandler<CreateJobsByProposalsCommand, Unit>
{

  private readonly JobsRepository _jobsRepository;

  public CreateJobsByProposalsCommandHandler(JobsRepository jobsRepository)
  {
    _jobsRepository = jobsRepository;
  }

  public async Task<Unit> Handle(CreateJobsByProposalsCommand request, CancellationToken cancellationToken)
  {
    var jobs = (from proposal in request.proposals select new Job(proposal)).ToList();

    await _jobsRepository.CreateMany(jobs);

    return new Unit();
  }
}