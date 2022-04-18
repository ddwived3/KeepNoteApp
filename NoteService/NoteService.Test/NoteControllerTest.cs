using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using NoteService.API.Service;
using NoteService.API.Models;
using NoteService.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace NoteService.Test
{
    public class NoteControllerTest
    {
        [Fact]
        public void GetByNoteIdShouldReturnOk()
        {
            int noteId = 101;
            Note note = new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "All about sports", CreationDate = new DateTime() };
            var mockService = new Mock<INoteService>();
            mockService.Setup(service => service.GetNoteById(noteId)).Returns(note);
            var controller = new NoteController(mockService.Object);

            var actual = controller.Get(noteId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<Note>(actionReult.Value);
        }

        [Fact]
        public void GetByUserIdShouldReturnAList()
        {
            string userId = "Devendra";
            
            var mockService = new Mock<INoteService>();
            mockService.Setup(service => service.GetAllNotesByUserId(userId)).Returns(this.GetNotes());
            var controller = new NoteController(mockService.Object);

            var actual = controller.Get(userId);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<List<Note>>(actionReult.Value);
        }

        [Fact]
        public void DeleteShouldReturnOK()
        {
            var mockService = new Mock<INoteService>();
            //Note note = new Note { Title = "Sample", NoteId = 1 };
            mockService.Setup(service => service.DeleteNote(101)).Returns(true);
            var controller = new NoteController(mockService.Object);

            var actual = controller.Delete(101);

            var actionReult = Assert.IsType<OkObjectResult>(actual);
            var actualValue = actionReult.Value;
            var expected = true;
            Assert.Equal(expected, actualValue);
        }

        [Fact]
        public void POSTShouldReturnCreated()
        {
            var mockService = new Mock<INoteService>();
            Note note = new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "All about sports", CreationDate = new DateTime() };

            mockService.Setup(service => service.CreateNote(note)).Returns(note);
            var controller = new NoteController(mockService.Object);

            var actual = controller.Post(note);

            var actionReult = Assert.IsType<CreatedResult>(actual);
            var actualValue = actionReult.Value;
            Assert.IsAssignableFrom<Note>(actualValue);
        }
        private List<Note> GetNotes()
        {
            List<Note> note = new List<Note> { new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "All about sports", CreationDate = new DateTime() } };

            return note;
        }
    }
}
