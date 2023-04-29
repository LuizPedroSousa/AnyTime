namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Modules.Platforms;
using AnyTime.Core.Domain.Shared;

public enum AnnouncementStatus
{
  Open,
  Closed
}


public class Announcement : BaseEntity
{
  public string title { get; set; }
  public string description { get; set; }
  public List<string> tags { get; set; }
  public string url { get; set; }
  public AnnouncementStatus status { get; set; }
  public Author author { get; set; }
  public Platform platform { get; set; }
  public DateTime publishedAt { get; set; }
  public string full_url => platform?.url + url;

  public Announcement()
  {
  }

  public Announcement(string title, string description, List<string> tags, string url, Platform platform)
  {
    this.title = title;
    this.description = description;
    this.tags = tags;
    this.url = url;
    this.author = author;
    this.platform = platform;
  }

  public Announcement(string title, string description, AnnouncementStatus status, List<string> tags, string url, Platform platform) : this(title, description, tags, url, platform)
  {
    this.status = status;
  }


  public Announcement(string title, string description, List<string> tags, string url, Author author, Platform platform) : this(title, description, tags, url, platform)
  {
    this.author = author;
  }

  public Announcement(string title, string description, AnnouncementStatus status, List<string> tags, string url, Author author, Platform platform, DateTime publishedAt)
  {
    this.title = title;
    this.description = description;
    this.status = status;
    this.tags = tags;
    this.url = url;
    this.author = author;
    this.platform = platform;
    this.publishedAt = publishedAt;
  }

  public override string ToString()
  {
    return $@"
      id: {id}
      title: {title} 
      author: {author.name}
      description: {description}
      ";
  }

  public static string ToString(List<Announcement> announcements, int from, int to)
  {
    var result = "";

    for (int i = from; i < to; i++)
    {
      result += $@"
      {announcements[i].ToString()}
      ";
    }

    return result;
  }
}