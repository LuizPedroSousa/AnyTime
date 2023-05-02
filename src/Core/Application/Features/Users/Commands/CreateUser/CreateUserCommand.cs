
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Core.Domain.Shared;
using MediatR;

namespace AnyTime.Core.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Either<BaseException, User>>
{
  public CreateUserDTO dto { get; set; }
}