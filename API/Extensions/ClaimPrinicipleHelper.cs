using Core.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimPrinicipleHelper
    {
        public static string GetUserEmail(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.Email);
        } 
    }
}
