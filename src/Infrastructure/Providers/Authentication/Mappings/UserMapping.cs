using AutoMapper;

namespace AnyTime.Infrastructure.Providers.Authentication.Mappings;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Infrastructure.Providers.Authentication.Models;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<User, UserModel>();
    CreateMap<UserModel, User>();
    CreateMap<CreateUserDTO, UserModel>();
    CreateMap<CreateUserDTO, User>();
  }
}
