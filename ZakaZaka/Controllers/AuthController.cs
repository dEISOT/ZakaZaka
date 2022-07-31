using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ZakaZaka.Models;
using ZakaZaka.Models.Request;
using ZakaZaka.Models.Response;
using ZakaZaka.Services.Contracts;

namespace ZakaZaka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController (IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.AuthenticateAsync(model);
                return Ok(result);
            }


            return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userService.FindByEmailAsync(model.Email);
                if (existingUser is null)
                {
                    var result = await _userService.AddUserAsync(model);
                    return Ok(result);
                }
            }
            return BadRequest();
        }
    }
}
