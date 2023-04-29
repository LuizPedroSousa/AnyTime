using AnyTime.Core.Domain.Shared;

namespace AnyTime.Core.Domain.Modules.Platforms;

public class Platform : BaseEntity
{
  public string name { get; set; }
  public string icon_url { get; set; }
  public string url { get; set; }
  public string thumb_url { get; set; }

}