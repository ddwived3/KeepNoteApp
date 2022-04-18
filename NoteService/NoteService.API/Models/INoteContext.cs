using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace NoteService.API.Models
{
    public interface INoteContext
    {
        IMongoCollection<Note> Notes { get; }
    }
}
