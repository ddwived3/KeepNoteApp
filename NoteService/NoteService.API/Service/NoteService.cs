using System;
using System.Collections.Generic;
using NoteService.API.Models;
using NoteService.API.Repository;
using NoteService.API.Exceptions;
using MongoDB.Driver;

namespace NoteService.API.Service
{
    public class NoteService : INoteService
    {
        INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public Note CreateNote(Note note)
        {
            var result = _noteRepository.CreateNote(note);
            if(result == null)
            {
                throw new NoteNotCreatedException("This note id already exists");
            }

            return result;
        }

        public bool DeleteNote(int noteId)
        {
            var result = _noteRepository.DeleteNote(noteId);
            if (!result)
            {
                throw new NoteNotFoundException("This note id not found");
            }

            return result;
        }

        public List<Note> GetAllNotesByUserId(string userId)
        {
            return _noteRepository.GetAllNotesByUserId(userId);
        }

        public Note GetNoteById(int noteId)
        {
            var result = _noteRepository.GetNoteById(noteId);
            if (result == null)
            {
                throw new NoteNotFoundException("This note id not found");
            }

            return result;
        }

        public bool UpdateNote(int noteId, Note note)
        {
            var result = _noteRepository.UpdateNote(noteId, note);
            if (!result)
            {
                throw new NoteNotFoundException("This note id not found");
            }

            return result;
        }

        public int GetMaxNoteId()
        {
            return _noteRepository.GetMaxNoteId();
        }
    }
}
