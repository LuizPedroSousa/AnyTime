using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

public class ScrapFreelancerBrAnnouncementsQuery : IRequest<IReadOnlyList<Announcement>>
{
  public bool headless { get; set; }
}
