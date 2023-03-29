namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public class Proposal : BaseEntity
{
  public int price { get; set; }
  public string description { get; set; }

  public Announcement announcement { get; set; }

  public Proposal(int price, string description)
  {
    this.price = price;
    this.description = description;
  }

  public Proposal(int price, string description, Announcement announcement) : this(price, description)
  {
    this.announcement = announcement;
  }
}