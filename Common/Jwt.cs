﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common.Enums;

namespace Common;

public static class Jwt
{
    public static int GetUserId(this string authHeader)
    {
        return Convert.ToInt32(authHeader.GetValueFromJwtClaims("Id"));
    }

    public static Roles GetRole(this string authHeader) =>
        Enum.Parse<Roles>(authHeader.GetValueFromJwtClaims(ClaimsIdentity.DefaultRoleClaimType));

    private static string? GetValueFromJwtClaims(this string authHeader, string type)
    {
        var encodedJwt = new JwtSecurityTokenHandler().ReadJwtToken(authHeader.Split(" ")[1]);
        return encodedJwt.Payload.Claims.FirstOrDefault(c => c.Type.ToString() == type)?.Value;
    }
    
    private static string GetToken(string authHeader)
    {
        return authHeader.Split(" ")[1];
    }
}
