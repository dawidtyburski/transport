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

            if(await roleManager.RoleExistsAsync("User") == false)
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
        }



        


    }

}
