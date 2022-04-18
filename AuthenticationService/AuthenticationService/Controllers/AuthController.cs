using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Controllers
{
    [EnableCors("angui")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService service;

        public AuthController(IAuthService _service)
        {
            this.service = _service;
        }

        [HttpGet]        
        public IActionResult Get()
        {
            return Ok("Calling");
        }

        // POST api/<controller>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                return Created("auth/register",service.RegisterUser(user));
            }
            catch (UserNotCreatedException ex)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                var result = service.LoginUser(user.UserId, user.Password);
                var token = result != null ? GetJWTToken(user.UserId) : null;
                return Ok(token);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        private string GetJWTToken(string userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_mongoapi_jwt"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "AuthService",
                audience: "noteapi",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds
            );

            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return JsonConvert.SerializeObject(response);
        }
    }
}
