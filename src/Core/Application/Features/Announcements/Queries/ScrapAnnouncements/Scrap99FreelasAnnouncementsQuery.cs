using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ScrapAnnouncements;

using AnyTime.Core.Domain.Modules.Jobs;

public class Scrap99FreelasAnnouncementsQuery : IRequest<List<Announcement>>
{
  public bool headless { get; set; }
}