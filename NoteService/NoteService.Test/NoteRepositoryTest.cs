using System;
using System.Collections.Generic;
using System.Text;
using NoteService.API.Models;
using NoteService.API.Repository;
using Xunit;

namespace NoteService.Test
{
    public class NoteRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;
        private INoteRepository repository;

        public NoteRepositoryTest(DatabaseFixture _fixture)
        {
            fixture = _fixture;
            repository = new NoteRepository(fixture.context);
        }
        [Fact]
        public void CreateNoteShouldReturnNote()
        {
            Note note = new Note { Id = 121, Title = "Entertainment", CreatedBy = "Sanjeev", Description = "All about entertainment", CreationDate = new DateTime() };

            var actual = repository.CreateNote(note);
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<Note>(actual);
        }

        [Fact]
        public void GetNoteByUserShouldReturnListOfnote()
        {
            string userId = "Devendra";
            var actual = repository.GetAllNotesByUserId(userId);

            Assert.IsAssignableFrom<List<Note>>(actual);
        }

        [Fact]
        public void GetNoteByIdShouldReturnNote()
        {
            var actual = repository.GetNoteById(101);

            Assert.IsAssignableFrom<Note>(actual);
            Assert.Equal("Sports", actual.Title);
        }

        [Fact]
        public void DeleteNoteShouldReturnTrue()
        {
            var actual = repository.DeleteNote(102);

            Assert.True(actual);
        }

        [Fact]
        public void UpdateNoteShouldReturnTrue()
        {
            Note note = new Note { Id = 101, Title = "Soprts", CreatedBy = "Devendra", Description = "Olympic Games", CreationDate = new DateTime() };

            var actual = repository.UpdateNote(101, note);
            Assert.True(actual);
        }
    }
}
