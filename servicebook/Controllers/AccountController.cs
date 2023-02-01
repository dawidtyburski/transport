using Microsoft.AspNetCore.Mvc;
using transport.Models;
using transport.Services;

namespace transport.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
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
    }
}
