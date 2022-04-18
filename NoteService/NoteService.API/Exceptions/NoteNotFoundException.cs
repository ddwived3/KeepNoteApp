using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.API.Exceptions
{
    public class NoteNotFoundException:ApplicationException
    {
        public NoteNotFoundException() { }
        public NoteNotFoundException(string message) : base(message) { }
    }
}
