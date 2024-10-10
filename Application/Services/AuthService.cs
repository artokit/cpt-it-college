using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.Services;
using Common;
using Common.Enums;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    
    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task<SuccessLoginResponseDto> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        var isRole = Enum.TryParse(registerUserRequestDto.Role, true, out Roles _);
        
        if (!isRole)
        {
            throw new UnknownRoleException("Неизвестная роль");
        }

        if (await userRepository.GetUserByEmail(registerUserRequestDto.Email) is not null)
        {
            throw new EmailIsExistingException("Данный email уже занят");
        }
        
        var hashedPassword = HashPassword.GetHashPassword(registerUserRequestDto.Password);
        var user = await userRepository.AddUser(registerUserRequestDto.Email, hashedPassword, registerUserRequestDto.Role);
        return new SuccessLoginResponseDto{ RefreshToken = GenerateRefreshToken(user), AccessToken = GenerateAccessToken(user)};
    }
    
    public async Task<SuccessLoginResponseDto> Login(LoginUserRequestDto loginUserRequestDto)
    {
        var user = await userRepository.GetUserByEmail(loginUserRequestDto.Email);
        
        if (user is null)
        {
            throw new FailedLoginException("Неверный логин или пароль");
        }

        if (!HashPassword.VerifyHashedPassword(loginUserRequestDto.Password, user.HashedPassword))
        {
            throw new FailedLoginException("Неверный логин или пароль");
        }

        return new SuccessLoginResponseDto {RefreshToken = GenerateRefreshToken(user), AccessToken = GenerateAccessToken(user)};
    }

    public async Task<SuccessLoginResponseDto> RefreshToken(string refreshToken)
    {
        
        var user = await userRepository.GetUserById(refreshToken.GetUserId());

        if (user is null)
        {
            throw new UserIsDeleteException("Данного пользователя нету в системе");
        }

        return new SuccessLoginResponseDto
        {
            RefreshToken = GenerateRefreshToken(user), AccessToken = GenerateAccessToken(user)
        };
        
    }
    
    private static string GenerateToken(List<Claim> claims, int timeExpireInMinute)
    {
        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(timeExpireInMinute)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    private static string GenerateAccessToken(DbUser user) => GenerateToken(GetClaims(user), AuthOptions.ACCESS_TOKEN_LIFETIME);

    private static string GenerateRefreshToken(DbUser user) =>
        GenerateToken(GetClaims(user), AuthOptions.REFRESH_TOKEN_LIFETIME);

    private static List<Claim> GetClaims(DbUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimType.Id.ToString(), user.Id.ToString()),
            new (ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return claims;
    }
}
