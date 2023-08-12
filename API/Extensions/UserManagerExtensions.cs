using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> GetUserWithAddressByClaims(this UserManager<AppUser> userManager ,ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);

            return await userManager.Users.Include(user => user.Address).FirstOrDefaultAsync(user => user.Email == email);
        }

        public static async Task<AppUser> GetUserByClaims(this UserManager<AppUser> userManager, ClaimsPrincipal claims)
        {
            var email = claims.FindFirstValue(ClaimTypes.Email);

            return await userManager.FindByEmailAsync(email);
        }

    }
}
