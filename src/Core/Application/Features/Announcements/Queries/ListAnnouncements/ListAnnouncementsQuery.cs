using AnyTime.Core.Domain.Modules.Announcements;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.ListAnnouncements;

public class ListAnnouncementsQuery : IRequest<IReadOnlyList<Announcement>>
{

}