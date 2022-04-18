using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using NoteService.API.Models;
using NoteService.API.Repository;
using NoteService.API.Exceptions;
using MongoDB.Driver;

namespace NoteService.Test
{
    public class NoteServiceTest
    {
        #region Positive tests
        [Fact]
        public void CreateNoteShouldReturnNote()
        {
            var mockRepo = new Mock<INoteRepository>();
            Note note = new Note { Id = 121, Title = "Entertainment", CreatedBy = "Sanjeev", Description = "All about entertainment", CreationDate = new DateTime() };
            mockRepo.Setup(repo => repo.CreateNote(note)).Returns(note);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual = service.CreateNote(note);

            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Note>(actual);
        }

        [Fact]
        public void GetNoteByUserShouldReturnListOfnote()
        {
            var mockRepo = new Mock<INoteRepository>();
            var userId = "Devendra";
            mockRepo.Setup(repo => repo.GetAllNotesByUserId(userId)).Returns(this.GetNotes());
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual = service.GetAllNotesByUserId(userId);

            Assert.IsAssignableFrom<List<Note>>(actual);
        }

        [Fact]
        public void GetNoteByIdShouldReturnNote()
        {
            var mockRepo = new Mock<INoteRepository>();
            var Id = 101;
            Note note = new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "All about sports", CreationDate = new DateTime() };

            mockRepo.Setup(repo => repo.GetNoteById(Id)).Returns(note);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual = service.GetNoteById(Id);

            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal("Sports", actual.Title);
        }

        [Fact]
        public void DeleteNoteShouldReturnTrue()
        {
            var mockRepo = new Mock<INoteRepository>();
            var Id = 102;

            mockRepo.Setup(repo => repo.DeleteNote(Id)).Returns(true);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);


            var actual = service.DeleteNote(Id);

            Assert.True(actual);
        }

        [Fact]
        public void UpdateNoteShouldReturnTrue()
        {
            int Id = 101;
            Note note = new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "Olympic Games", CreationDate = new DateTime() };

            var mockRepo = new Mock<INoteRepository>();

            mockRepo.Setup(repo => repo.UpdateNote(Id,note)).Returns(true);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);


            var actual = service.UpdateNote(Id, note);
            Assert.True(actual);
        }

        private List<Note> GetNotes()
        {
            List<Note> notes = new List<Note> {
                new Note{Id=101, Title="Sports", CreatedBy="Devendra", Description="All about sports", CreationDate=new DateTime() },
                 new Note{Id=102, Title="Politics", CreatedBy="Devendra", Description="INDIAN politics", CreationDate=new DateTime() }
            } ;

            return notes;
        }

        #endregion Positive tests

        #region Negative tests

        [Fact]
        public void CreateNoteShouldThrowException()
        {
            var mockRepo = new Mock<INoteRepository>();
            Note note = new Note { Id = 101, Title = "Sports", CreatedBy = "Devendra", Description = "All about sports", CreationDate = new DateTime() };
            mockRepo.Setup(repo => repo.GetNoteById(101)).Returns(note);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual = Assert.Throws<NoteNotCreatedException>(()=> service.CreateNote(note));
            Assert.Equal("This note id already exists",actual.Message);
        }

        
        [Fact]
        public void GetNoteByUserShouldReturnEmptyList()
        {
            var mockRepo = new Mock<INoteRepository>();
            var userId = "Devendra";
            mockRepo.Setup(repo => repo.GetAllNotesByUserId(userId)).Returns(new List<Note>());
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual = service.GetAllNotesByUserId(userId);

            Assert.IsAssignableFrom<List<Note>>(actual);
            Assert.Empty(actual);
        }

        
        [Fact]
        public void GetNoteByIdShouldThrowException()
        {
            var mockRepo = new Mock<INoteRepository>();
            var Id = 105;
            Note note = null;
            mockRepo.Setup(repo => repo.GetNoteById(Id)).Returns(note);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);

            var actual =Assert.Throws<NoteNotFoundException>(()=>  service.GetNoteById(Id));

            Assert.Equal("This note id not found", actual.Message);
        }

       
        [Fact]
        public void DeleteNoteShouldThrowException()
        {
            var mockRepo = new Mock<INoteRepository>();
            var Id = 105;

            mockRepo.Setup(repo => repo.DeleteNote(Id)).Returns(false);
            var service = new NoteService.API.Service.NoteService(mockRepo.Object);


            var actual = Assert.Throws<NoteNotFoundException>(()=> service.DeleteNote(Id));

            Assert.Equal("This note id not found", actual.Message);
        }
        
       [Fact]
       public void UpdateNoteShouldThrowException()
       {
           int Id = 105;
           Note note = new Note { Id = 105, Title = "Sports", CreatedBy = "Devendra", Description = "Olympic Games", CreationDate = new DateTime() };

           var mockRepo = new Mock<INoteRepository>();

           mockRepo.Setup(repo => repo.UpdateNote(Id, note)).Returns(false);
           var service = new NoteService.API.Service.NoteService(mockRepo.Object);


           var actual = Assert.Throws<NoteNotFoundException>(() => service.UpdateNote(Id, note));
            Assert.Equal("This note id not found", actual.Message);
        }

        #endregion Negative tests
    }
}
