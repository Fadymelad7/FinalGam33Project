using Gma33.Core.Entites.IdentityEntites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GAM33.Helpers
{
    public static class GetCuurentUserWithAddressExtension
    {
        public static async Task<ApplicationUser> FindEamilWithAddressAsync(this UserManager<ApplicationUser> usermanager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await usermanager.Users.Where(u => u.Email == email).Include(u => u.Address).FirstOrDefaultAsync();

            return user;
        }
    }
}
