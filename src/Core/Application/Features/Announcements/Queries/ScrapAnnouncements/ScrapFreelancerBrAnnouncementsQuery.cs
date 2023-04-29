using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Modules.Platforms;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

public class ScrapFreelancerBrAnnouncementsQuery : IRequest<IReadOnlyList<Announcement>>
{
  public bool headless { get; set; }
  public Platform platform { get; set; }
}
