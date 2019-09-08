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
        //  private TemporaryDataContext _db;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Method to login a user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var jwtToken = _accountService.Login(loginDto);

                if (jwtToken == null)
                {
                    return Unauthorized();
                }

                return Ok(jwtToken);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Method to register a user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterDto login)
        {
            if (ModelState.IsValid)
            {

                if (!TemporaryDataContext._users.Where(x => (x.UserName == login.Username)).Any())// if the login user name is doesnot exist in the list
                {
                    TemporaryDataContext._users.Append(new User { Id = TemporaryDataContext._users.Count() + 1, Password = login.Password, UserName = login.Username });

                    return Ok("User successfully added");
                }
            }

            return BadRequest("Username already exist!!");

        }


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Get()
        {
            var userrs = TemporaryDataContext._users.ToList();
            return Ok(userrs);
        }
    }
}