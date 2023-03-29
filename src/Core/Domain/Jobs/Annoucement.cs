namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public class Announcement : BaseEntity
{
  public string title { get; set; }
  public string description { get; set; }
  public List<string> tags { get; set; }

  public string url { get; set; }
  public Author author { get; set; }

  public Announcement(string title, string description, List<string> tags, string url)
  {
    this.title = title;
    this.description = description;
    this.tags = tags;
    this.url = url;
    this.author = author;
  }

  public Announcement(string title, string description, List<string> tags, string url, Author author) : this(title, description, tags, url)
  {
    this.author = author;
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