using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using transport.Models;
using transport;
using System.Data;

public static class DataSeeder
{
    public static async void Initialize(IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = (UserManager<CustomUser>)scope.ServiceProvider.GetService(typeof(UserManager<CustomUser>));
            var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

            if (await roleManager.RoleExistsAsync("User") == false)
            {
                IdentityRole newRole = new IdentityRole("User");
                await roleManager.CreateAsync(newRole);
            }
            if (await roleManager.RoleExistsAsync("Admin") == false)
            {
                IdentityRole newRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(newRole);
            }
            if (await roleManager.RoleExistsAsync("SuperAdmin") == false)
            {
                IdentityRole newRole = new IdentityRole("SuperAdmin");
                await roleManager.CreateAsync(newRole);
            }

            CustomUser superadmin = await userManager.FindByEmailAsync("superadmin@cargo.eu");
            if (superadmin == null)
            {
                var user = new CustomUser
                {
                    UserName = "superadmin@cargo.eu",
                    Email = "superadmin@cargo.eu",
                    FirstName = "Dawid",
                    LastName = "Tyburski",
                    FullName = "Dawid Tyburski",
                    PhoneNumber = "788367337",
                    Counter = 0,
                    isBlocked = false,
                };

                await userManager.CreateAsync(user, "Secret123@");
                await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                await userManager.AddToRoleAsync(user, "SuperAdmin");
            }

            CustomUser admin = await userManager.FindByEmailAsync("admin@cargo.eu");
            if (admin == null)
            {
                var user = new CustomUser
                {
                    UserName = "admin@cargo.eu",
                    Email = "admin@cargo.eu",
                    FirstName = "Adam",
                    LastName = "Dudek",
                    FullName = "Adam Dudek",
                    PhoneNumber = "531355332",
                    Counter = 0,
                    isBlocked = false,
                };

                await userManager.CreateAsync(user, "Secret123@");
                await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                await userManager.AddToRoleAsync(user, "Admin");
            }

            CustomUser user1 = await userManager.FindByEmailAsync("ppotoczak@cargo.eu");
            if (user1 == null)
            {
                var user = new CustomUser
                {
                    UserName = "ppotoczak@cargo.eu",
                    Email = "ppotoczak@cargo.eu",
                    FirstName = "Paweł",
                    LastName = "Potoczak",
                    FullName = "Paweł Potoczak",
                    PhoneNumber = "694561231",
                    Counter = 0,
                    isBlocked = false,
                };

                await userManager.CreateAsync(user, "Secret123@");
                await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                await userManager.AddToRoleAsync(user, "User");
            }

            CustomUser user2 = await userManager.FindByEmailAsync("akowalski@cargo.eu");
            if (user2 == null)
            {
                var user = new CustomUser
                {
                    UserName = "akowalski@cargo.eu",
                    Email = "akowalski@cargo.eu",
                    FirstName = "Adrian",
                    LastName = "Kowalski",
                    FullName = "Adrian Kowalski",
                    PhoneNumber = "597521339",
                    Counter = 0,
                    isBlocked = true,
                };

                await userManager.CreateAsync(user, "Secret123@");
                await userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}
