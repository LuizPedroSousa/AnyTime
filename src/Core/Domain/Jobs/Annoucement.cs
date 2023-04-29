namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public enum AnnouncementPlatform
{
  NineNineFreelas,
  FreelancerBR
}

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

  public AnnouncementStatus status { get; set; }

  public string url { get; set; }
  public Author author { get; set; }


  public AnnouncementPlatform platform { get; set; }

  public DateTime publishedAt { get; set; }

  public Announcement(string title, string description, List<string> tags, string url, AnnouncementPlatform platform)
  {
    this.title = title;
    this.description = description;
    this.tags = tags;
    this.url = url;
    this.author = author;
    this.platform = platform;
  }

  public Announcement(string title, string description, AnnouncementStatus status, List<string> tags, string url, AnnouncementPlatform platform) : this(title, description, tags, url, platform)
  {
    this.status = status;
  }


  public Announcement(string title, string description, List<string> tags, string url, Author author, AnnouncementPlatform platform) : this(title, description, tags, url, platform)
  {
    this.author = author;
  }

  public Announcement(string title, string description, AnnouncementStatus status, List<string> tags, string url, Author author, AnnouncementPlatform platform, DateTime publishedAt)
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