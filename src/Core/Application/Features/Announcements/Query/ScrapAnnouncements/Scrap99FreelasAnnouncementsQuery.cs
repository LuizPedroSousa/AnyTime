using AnyTime.Core.Domain.Modules.Jobs;
using MediatR;

namespace AnyTime.Application.Features.Announcements.Query.ScrapAnnouncements;

public class Scrap99FreelasAnnouncementsQuery : IRequest<List<Announcement>>
{
  public bool headless { get; set; }
}