using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AnyTime.Infrastructure.Providers.Authentication;

using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.DTOs.Validators;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Models.Settings;
using AnyTime.Core.Application.Contracts.Providers.AutheticationProvider.Responses;
using AnyTime.Core.Domain.Modules.Users;
using AnyTime.Core.Domain.Shared;
using AnyTime.Infrastructure.Providers.Authentication.Models;

internal class IdentityAuthenticationProvider : AuthenticationProvider
{
  private readonly UserManager<UserModel> _userMananager;
  private readonly SignInManager<UserModel> _signInManager;
  private readonly JwtSettings _jwtSettings;

  private readonly IMapper _mapper;

  public IdentityAuthenticationProvider(UserManager<UserModel> userMananager, SignInManager<UserModel> signInManager, IOptions<JwtSettings> jwtSettings, IMapper mapper)
  {
    _userMananager = userMananager;
    _signInManager = signInManager;
    _jwtSettings = jwtSettings.Value;
    _mapper = mapper;
  }

  public async Task<SignInResponse> SignIn(AuthenticateUserDTO credentials)
  {
    var credentialsValidator = new AuthenticateUserValidator();

    var validate = await credentialsValidator.ValidateAsync(credentials);

    if (!validate.IsValid)
    {
      return new InvalidException(validate.Errors[0].ErrorMessage);
    }


    var userExists = await _userMananager.FindByEmailAsync(credentials.email);

    if (userExists is null)
    {
      return new NotFoundException("User not found");
    }

    var validatePassword = await _signInManager.CheckPasswordSignInAsync(userExists, credentials.password, false);

    if (!validatePassword.Succeeded)
    {
      return new InvalidException("Invalid user credentials");
    }

    var access_token = await GenerateToken(userExists);

    return new SignInResult
    {
      token = new Token { access_token = access_token },
      user = _mapper.Map<User>(userExists)
    };
  }

  public async Task<SignUpResponse> SignUp(CreateUserDTO credentials)
  {
    var credentialsValidator = new CreateUserValidator();

    var validate = await credentialsValidator.ValidateAsync(credentials);

    if (!validate.IsValid)
    {
      return new InvalidException(validate.Errors[0].ErrorMessage);
    }

    var userExists = await _userMananager.FindByEmailAsync(credentials.email);

    if (userExists is not null)
    {
      return new InvalidException("User already exists");
    }

    var user = this._mapper.Map<UserModel>(credentials);

    var result = await this._userMananager.CreateAsync(user, credentials.password);

    if (!result.Succeeded)
    {
      return new InvalidException(result.Errors.FirstOrDefault().Description);
    }

    await _userMananager.AddToRoleAsync(user, UserRoles.freelancer);

    return this._mapper.Map<User>(user);
  }

  private async Task<string> GenerateToken(UserModel user)
  {


    var userClaim = await _userMananager.GetClaimsAsync(user);
    var userRoles = await _userMananager.GetRolesAsync(user);

    var roleClaims = (from role in userRoles
                      select new Claim(ClaimTypes.Role, role)).ToList();

    var claims = new[]{
      new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim("user_id", user.Id)
    }.Union(userClaim).Union(roleClaims);

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));

    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: _jwtSettings.issuer,
      audience: _jwtSettings.audience,
      expires: DateTime.UtcNow.AddMinutes(_jwtSettings.duration_in_minutes),
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

}