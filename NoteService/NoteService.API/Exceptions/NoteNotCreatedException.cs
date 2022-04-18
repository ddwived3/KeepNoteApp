using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.API.Exceptions
{
    public class NoteNotCreatedException:ApplicationException
    {
        public NoteNotCreatedException() { }
        public NoteNotCreatedException(string message) : base(message) { }
    }
}
