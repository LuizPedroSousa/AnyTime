using AnyTime.API.Shared.DTOs;
using AnyTime.Core.Domain.Modules.Announcements;
using AnyTime.Core.Domain.Modules.Platforms;

namespace AnyTime.API.Modules.Announcements.Controllers.DTOs;

public class AnnouncementDTO : BaseDTO
{
  public string status { get; set; }
  public string title { get; set; }
  public string description { get; set; }
  public List<string> tags { get; set; }
  public string url { get; set; }
  public Author author { get; set; }
  public Platform platform { get; set; }
  public DateTime publishedAt { get; set; }
  public string full_url { get; set; }
}
