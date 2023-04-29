using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.FilterAnnouncements;

using AnyTime.Core.Domain.Modules.Jobs;

public class FilterAnnouncementsListWithNLPQuery : IRequest<IReadOnlyList<Announcement>>
{
  public IReadOnlyList<Announcement> announcements { get; set; }
}