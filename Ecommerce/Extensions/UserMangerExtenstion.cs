using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecommerce.Extensions
{
    public static class UserMangerExtenstion
    {
        public static async Task<ApplicationUser?> GetUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal userClaim)
        {
            var Email =userClaim.FindFirstValue(ClaimTypes.Email);
            var User = await userManager.Users.Include(a=>a.Address).FirstOrDefaultAsync(e=>e.Email ==Email);
            return User;
        }
    }
}
