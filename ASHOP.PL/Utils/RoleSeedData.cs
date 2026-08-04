using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASHOP.PL.Utils
{
    public class RoleSeedData : ISeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task DataSeed()
        {string [] Roles = {"SuperAdmin","Admin","user" };
            if (!await _roleManager.Roles.AnyAsync())
            {
                foreach(var role in Roles)
                {
                await _roleManager.CreateAsync(new IdentityRole(role));

                }
            }
        }
    }
}
