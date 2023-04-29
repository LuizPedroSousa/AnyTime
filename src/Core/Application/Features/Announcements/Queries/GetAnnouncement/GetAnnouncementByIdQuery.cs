using AnyTime.Core.Domain.Modules.Jobs;
using AnyTime.Core.Domain.Shared;
using MediatR;

namespace AnyTime.Core.Application.Features.Announcements.Queries.GetAnnouncement;

public class GetAnnouncementByIdQuery : IRequest<Either<NotFoundException, Announcement>>
{
  public string id { get; set; }
}