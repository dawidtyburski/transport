using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Reflection.Metadata;
using System.Security.Claims;
using transport.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace transport.Controllers
{
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<CustomUser> _userManager;
        private SignInManager<CustomUser> _signInManager;
        public RoleManager<IdentityRole> _roleManager;
        private readonly transportDbContext _dbContext;

        public AccountController(UserManager<CustomUser> userManager,
                                SignInManager<CustomUser> signInManager, 
                                RoleManager<IdentityRole> roleManager,
                                transportDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        
        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(ErrorMessage = "Please enter the security code as a number.",
                    CaptchaGeneratorLanguage = Language.English,
                    CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
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
                    PhoneNumber = model.PhoneNumber,
                    Counter = 0,
                    isBlocked = false
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddClaimAsync(user, new Claim("FullName", user.FullName));

                if (result.Succeeded)
                {
                    var role = _roleManager.FindByNameAsync("User").Result;

                    if (role != null)
                    {
                        IdentityResult roleresult = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    TempData["RegisterSuccess"] = "Account created successfully";
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
        [ValidateDNTCaptcha(ErrorMessage = "Please enter the security code as a number.",
                    CaptchaGeneratorLanguage = Language.English,
                    CaptchaGeneratorDisplayMode = DisplayMode.NumberToWord)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext
                    .Users
                    .FirstOrDefault(u=>u.Email == loginModel.Email);
                if(!user.isBlocked)
                {
                    if ((await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
                else
                ModelState.AddModelError(string.Empty, "Your account is blocked");
            }           
            return View(loginModel);
        }

        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpGet("user/settings")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpPost("user/settings")]
        public async Task<IActionResult> ChangePassword([FromForm]ChangePasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if(result.Succeeded) 
                {
                    TempData["PasswordChangeSuccess"] = "Password Changed";
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }            
            return View(model);
        }

        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpGet("users/ranking")]
        public ActionResult<IEnumerable<UsersRankingModel>> GetUsersRanking()
        {
            var ranking = _dbContext
                    .Users
                    .OrderByDescending(r => r.Counter)
                    .ToList();

            var result = new List<UsersRankingModel>();
            for (int i = 0; i < ranking.Count; i++)
            {
                UsersRankingModel user = new UsersRankingModel()
                {
                    Name = ranking[i].FullName,
                    Email = ranking[i].Email,
                    Counter = ranking[i].Counter
                };
                result.Add(user);
            }
            return View(result);
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        [Authorize(Roles ="Admin, SuperAdmin")]
        [HttpGet("admin/panel")]
        public async Task<ActionResult<IEnumerable<AdminPanelModel>>> GetAdminPanel()
        {
            var id = _userManager.GetUserId(User);
            var adminusers = _dbContext
                    .Users
                    .Where(a=>a.Id != id)
                    .OrderByDescending(r => r.FullName)
                    .ToList();

            var result = new List<AdminPanelModel>();
            for (int i = 0; i < adminusers.Count; i++)
            {
                var roles = await _userManager.GetRolesAsync(adminusers[i]);
                var role = roles.FirstOrDefault(r => !string.IsNullOrEmpty(r)) ?? "";
            AdminPanelModel user = new AdminPanelModel()
            {
                Id = adminusers[i].Id,
                Email = adminusers[i].Email,
                FullName = adminusers[i].FullName,
                PhoneNumber = adminusers[i].PhoneNumber,
                Counter = adminusers[i].Counter,
                isBlocked = adminusers[i].isBlocked,
                Role = role,
            };               
                result.Add(user);
            }
            return View(result);
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpPost("admin/panel")]
        public IActionResult AdminActions(string id)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);
            if (!user.isBlocked)
            {
                user.isBlocked = true;
            }
            else user.isBlocked = false;
            _dbContext.SaveChanges();
            return RedirectToAction("GetAdminPanel");

        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("admin/panel/role/{id}/{fullname}/{role}")]
        public IActionResult ChangeRole(string id, string fullname, string email, string role) 
        {
            ChangeRoleModel model = new ChangeRoleModel()
            {
                ChangeRoleId= id,
                ActualRole = role,
                FullName= fullname,
                Email= email,
                Roles = new List<string>()
                {
                    "User",
                    "Admin",
                    "SuperAdmin"
                }
            };
            return View(model);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("admin/panel/role/{id}/{fullname}/{role}")]
        public async Task<IActionResult> ChangeRole(string id, [FromForm]ChangeRoleModel model)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == id);


            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles.ToArray());
                 await _userManager.AddToRoleAsync(user, model.Role);
                _dbContext.SaveChanges();
                return RedirectToAction("GetAdminPanel");
        }
    }
}
