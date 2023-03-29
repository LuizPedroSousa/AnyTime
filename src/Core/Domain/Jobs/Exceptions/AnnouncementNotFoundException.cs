namespace AnyTime.Core.Domain.Modules.Jobs.Exceptions;

using AnyTime.Core.Domain.Shared;

public class AnnouncementNotFoundException : BaseException
{
  public AnnouncementNotFoundException() : base("Announcement not found", StatusException.NotFound)
  {
  }
}