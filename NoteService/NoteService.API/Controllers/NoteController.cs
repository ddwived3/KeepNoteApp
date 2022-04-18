using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteService.API.Service;
using NoteService.API.Models;
using NoteService.API.Exceptions;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteService.API.Controllers
{
    [Authorize]
    [EnableCors("angui")]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteService _service;

        public NoteController(INoteService service)
        {
            _service = service;
        }

        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetMaxNoteId());
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // GET: api/<controller>
        [HttpGet("{noteId:int}")]
        public IActionResult Get(int noteId)
        {
            try
            {
                return Ok(_service.GetNoteById(noteId));
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
                return Ok(_service.GetAllNotesByUserId(userId));
            }
            catch (NoteNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Note note)
        {
            try
            {
                note.Id = _service.GetMaxNoteId();
                return Created("api/note", _service.CreateNote(note));
            }
            catch (NoteNotCreatedException ex)
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
        public IActionResult Put(int id, [FromBody]Note note)
        {
            try
            {
                return Ok(_service.UpdateNote(id, note));
            }
            catch (NoteNotFoundException ex)
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
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_service.DeleteNote(id));
            }
            catch (NoteNotFoundException ex)
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