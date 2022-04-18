using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CategoryService.API.Service;
using CategoryService.API.Models;
using CategoryService.API.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoryService.API.Controllers
{
    [Authorize]
    [EnableCors("angui")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        // GET: api/<controller>
        [HttpGet("{categoryId:int}")]
        public IActionResult Get(int categoryId)
        {
            try
            {
                return Ok(_service.GetCategoryById(categoryId));
            }
            catch 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // GET api/<controller>/5
        [HttpGet("{userId}")]
        public IActionResult Get(string userId)
        {
            try
            { 
                return Ok(_service.GetAllCategoriesByUserId(userId));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            try
            {
                category.Id = _service.GetMaxCategoryId();
                return Created("api/category", _service.CreateCategory(category));
            }
            catch (CategoryNotCreatedException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                //return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category category)
        {
            try
            { 
                return Ok(_service.UpdateCategory(id, category));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                //return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            { 
                return Ok(_service.DeleteCategory(id));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
