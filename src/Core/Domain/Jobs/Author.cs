namespace AnyTime.Core.Domain.Modules.Jobs;

using AnyTime.Core.Domain.Shared;

public class Author : BaseEntity
{
  public string name { get; set; }
  public string avatar_url { get; set; }
}