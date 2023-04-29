using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;

public class ScrapFreelancerBrAnnouncementQuery : IRequest<Either<BaseException, Announcement>>
{
  public string url { get; set; }
}
