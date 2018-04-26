using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebKantora.Data.Models;

namespace WebKantora.Data
{
    public static class RolesData
    {
        private static readonly string[] roles = new[] { "User", "Admin" };

        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var create = await roleManager.CreateAsync(new Role() { Name = role});

                    if (!create.Succeeded)
                    {
                        throw new Exception("Failed to create role");
                    }
                }
            }
        }
    }
}
