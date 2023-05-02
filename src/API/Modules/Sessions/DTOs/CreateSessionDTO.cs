namespace AnyTime.API.Modules.Sessions.DTOs;

public class UserDTO
{
  public string id { get; set; }
}

public class CreateSessionDTO
{
  public string access_token { get; set; }
  public UserDTO user { get; set; }
}