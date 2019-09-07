using InternetMoviesOnDemand.Data;
using InternetMoviesOnDemand.Dtos;
using InternetMoviesOnDemand.Entities;
using InternetMoviesOnDemand.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASPNetCoreJWTSample.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var jwtToken = _accountService.Login(loginDto);

            if (jwtToken == null)
            {
                return Unauthorized();
            }

            return Ok(jwtToken);
        }

        [HttpPost]
        public IActionResult Register(RegisterDto login)
        {
            if (!TemporaryDataContext._users.Where(x => (x.UserName == login.Username)).Any())// if the login user name is doesnot exist in the list
            {
                TemporaryDataContext._users.Append(new User { Id = TemporaryDataContext._users.Count() + 1, Password = login.Password, UserName = login.Username });

                return Ok("User successfully added");
            }
            else
            {
                return BadRequest("Username already exist!!");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userrs = TemporaryDataContext._users.ToList();
            return Ok(userrs);
        }
    }
}