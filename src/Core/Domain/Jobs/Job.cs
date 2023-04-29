using AnyTime.Core.Domain.Modules.Proposals;
using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Domain.Modules.Announcements;

public enum JobStatus
{
  Rejected,
  Pending,
  Sended,
  Accepted
}

public class Job : BaseEntity
{
  public JobStatus status { get; set; } = JobStatus.Pending;
  public Proposal proposal { get; set; }


  public Job(Proposal proposal)
  {
    this.proposal = proposal;
  }

  public Job()
  {
  }

  public Job(JobStatus status, Proposal proposal)
  {
    this.status = status;
    this.proposal = proposal;
  }
}