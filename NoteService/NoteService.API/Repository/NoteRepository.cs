using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Models;
using MongoDB.Driver;

namespace NoteService.API.Repository
{
    public class NoteRepository : INoteRepository
    {
        INoteContext _context;
        public NoteRepository(INoteContext context)
        {
            _context = context;
        }

        public Note CreateNote(Note note)
        {
            _context.Notes.InsertOne(note);
            return note;
        }

        public bool DeleteNote(int noteId)
        {
            return _context.Notes.DeleteOne(x => x.Id == noteId).IsAcknowledged;
        }

        public List<Note> GetAllNotesByUserId(string userId)
        {
            return _context.Notes.Find(x=>x.CreatedBy == userId).ToList();
        }

        public List<Note> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public int GetMaxNoteId()
        {
            var notes = _context.Notes.Find(_ => true).ToList();
            var maxId = (notes != null && notes.Count > 0) ? notes.Max(x => x.Id) : 0;
            return maxId + 1;
        }

        public Note GetNoteById(int noteId)
        {
            return _context.Notes.Find(x => x.Id == noteId).FirstOrDefault();
        }

        public bool UpdateNote(int noteId, Note note)
        {
            var model = Builders<Note>.Update
                .Set(x => x.Title, note.Title)
                .Set(x => x.Description, note.Description)
                .Set(x => x.CreationDate, note.CreationDate)
                .Set(x => x.Category, note.Category)
                .Set(x => x.CreatedBy, note.CreatedBy);
            var updateOptons = new UpdateOptions { IsUpsert = true };

            var result = _context.Notes.UpdateOne(x => x.Id == note.Id, model, updateOptons);
            return result.IsAcknowledged;
        }
    }
}
