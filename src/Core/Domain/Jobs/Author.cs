namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public class Author : BaseEntity
{
  public string name { get; set; }
  public string avatar_url { get; set; }

  public List<Announcement> announcements { get; set; }

  public Author(string name, string avatar_url)
  {
    this.name = name;
    this.avatar_url = avatar_url;
  }

  public Author(string name, string avatar_url, List<Announcement> announcements) : this(name, avatar_url)
  {
    this.announcements = announcements;
  }
}