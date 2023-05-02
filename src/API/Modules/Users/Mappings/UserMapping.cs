using AnyTime.API.Modules.Users.DTOs;
using AnyTime.Core.Domain.Modules.Users;
using AutoMapper;

namespace AnyTime.API.Modules.Users.Mappings;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<User, CreateUserDTO>();
  }
}
