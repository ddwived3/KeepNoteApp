using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryService.API.Models;
using MongoDB.Driver;

namespace CategoryService.API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ICategoryContext _context;
        public CategoryRepository(ICategoryContext context)
        {
            _context = context;
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.InsertOne(category);
            return category;
        }

        public bool DeleteCategory(int categoryId)
        {
            return _context.Categories.DeleteOne(x => x.Id == categoryId).IsAcknowledged;
        }

        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return _context.Categories.Find(x=>x.CreatedBy == userId || x.IsPublic == true).ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(x => x.Id == categoryId).FirstOrDefault();
        }

        public bool UpdateCategory(int categoryId, Category category)
        {
            var model = Builders<Category>.Update
                .Set(x => x.Name, category.Name)
                .Set(x => x.IsPublic, category.IsPublic)
                .Set(x => x.CreationDate, category.CreationDate)
                .Set(x => x.CreatedBy, category.CreatedBy);
            var updateOptons = new UpdateOptions { IsUpsert = true };

            var result = _context.Categories.UpdateOne(x => x.Id == category.Id, model, updateOptons);
            return result.IsAcknowledged;
        }

        public int GetMaxCategoryId()
        {
            var categories = _context.Categories.Find(_ => true).ToList();
            var maxId = (categories != null && categories.Count > 0) ? categories.Max(x => x.Id) : 0;
            return maxId + 1;
        }
    }
}
