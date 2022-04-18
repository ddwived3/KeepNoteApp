using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CategoryService.API.Models
{
    public class CategoryContext : ICategoryContext
    {
        private readonly IMongoDatabase database;
        MongoClient client;
        public CategoryContext(IConfiguration configuration)
        {
            client = new MongoClient(configuration.GetSection("MongoDB:ConnectionString").Value);
            database = client.GetDatabase(configuration.GetSection("MongoDB:Database").Value);
        }
        public IMongoCollection<Category> Categories => database.GetCollection<Category>("Categories");
    }
}
