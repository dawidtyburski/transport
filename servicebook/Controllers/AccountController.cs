using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using transport.Models;
using transport.Services;

namespace transport.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            var isCreated =_accountService.RegisterUser(dto);
            if(!isCreated)
            {
                return BadRequest("Email is taken");
            }
            return Ok();
        }
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        public ActionResult Login([FromForm] LoginDto dto)
        {
            bool result = _accountService.GenerateJwt(dto);
            if(!result)
            {
                TempData["LoginError"] = "Błędny email lub hasło";
                return View();
            }
            return Redirect("api/order/all");

        }
    }
}
