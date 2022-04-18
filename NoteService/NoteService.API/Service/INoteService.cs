using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Models;

namespace NoteService.API.Service
{
    public interface INoteService
    {
        Note CreateNote(Note note);
        bool DeleteNote(int noteId);
        bool UpdateNote(int noteId, Note note);
        Note GetNoteById(int noteId);
        List<Note> GetAllNotesByUserId(string userId);
        int GetMaxNoteId();
    }
}
