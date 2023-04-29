using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;

public class ScrapFreelancerBrAnnouncementQuery : IRequest<Either<BaseException, Announcement>>
{
  public string url { get; set; }
  public Platform platform { get; set; }
}
