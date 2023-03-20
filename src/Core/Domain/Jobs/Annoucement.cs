namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public class Announcement : BaseEntity
{
  public string title { get; set; }
  public string description { get; set; }
  public List<string> tags { get; set; }

  public string url { get; set; }
  public Author author { get; set; }

  public override string ToString()
  {
    return $@"
      id: {id}
      title: {title} 
      author: {author.name}
      description: {description}
      ";
  }

  public static string ToString(List<Announcement> announcements)
  {
    var result = "";

    foreach (Announcement announcement in announcements)
    {
      result += $@"
      {announcement.ToString()}
      ";
    }

    return result;
  }

}