using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;

public class Scrap99FreelasAnnouncementsQuery : IRequest<List<Announcement>>
{
  public bool headless { get; set; }
  public Platform platform { get; set; }
}