using AnyTime.API.Modules.Sessions.DTOs;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;
using AutoMapper;

namespace AnyTime.API.Modules.Sessions.Mappings;

public class SessionMapping : Profile
{
  public SessionMapping()
  {
    CreateMap<SignInResult, CreateSessionDTO>().ForMember(dto => dto.access_token, options =>
    {
      options.MapFrom(result => result.token.access_token);
    });
  }
}
