using System;
using CategoryService.API.Models;
using MongoDB.Driver;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace CategoryService.Test
{
    public class DatabaseFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public ICategoryContext context;
        public DatabaseFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new CategoryContext(configuration);
            context.Categories.InsertMany(new List<Category>
            {
                new Category{Id=101, Name="Sports", CreatedBy="Mukesh", IsPublic=true, CreationDate=new DateTime() },
                 new Category{Id=102, Name="Politics", CreatedBy="Mukesh", IsPublic=true, CreationDate=new DateTime() }
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
