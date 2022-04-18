using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.API.Controllers
{
    [Authorize]
    [EnableCors("angui")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]User user)
        {
            try
            {
                var result = _userService.RegisterUser(user);
                return Created("api/user", result);
            }
            catch (UserNotCreatedException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody]User user)
        {
            try
            {
                return Ok(_userService.UpdateUser(id, user));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                return Ok(_userService.DeleteUser(id));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }
    }
}
