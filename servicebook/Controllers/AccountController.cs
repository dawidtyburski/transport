using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using transport.Models;
using transport.Services;

namespace transport.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;

        public RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<CustomUser> userManager,
                                SignInManager<CustomUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = $"{model.FirstName} {model.LastName}",
                    PhoneNumber= model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));

                if (result.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync("Administrator") == false)
                    {
                        IdentityRole newRole = new IdentityRole("Administrator");
                        await _roleManager.CreateAsync(newRole);
                    }
                    if (await _roleManager.RoleExistsAsync("User") == false)
                    {
                        IdentityRole newRole = new IdentityRole("User");
                        await _roleManager.CreateAsync(newRole);
                    }
                    var role = _roleManager.FindByNameAsync("User").Result;

                    if (role != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {

                    if ((await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false)).Succeeded)
                    {                      
                        return Redirect("/Home/Index");
                    }
                                   
            }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(loginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
