using System;
using NoteService.API.Models;
using MongoDB.Driver;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace NoteService.Test
{
    public class DatabaseFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public INoteContext context;
        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new NoteContext(configuration);
            context.Notes.InsertMany(new List<Note>
            {
                new Note{Id=101, Title="Sports", CreatedBy="Devendra", Description="All about sports", CreationDate=new DateTime() },
                 new Note{Id=102, Title="Politics", CreatedBy="Devendra", Description="INDIAN politics", CreationDate=new DateTime() }
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
