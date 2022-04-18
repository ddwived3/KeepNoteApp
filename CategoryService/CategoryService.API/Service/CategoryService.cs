using System;
using System.Collections.Generic;
using CategoryService.API.Models;
using CategoryService.API.Repository;
using CategoryService.API.Exceptions;
using MongoDB.Driver;

namespace CategoryService.API.Service
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Category CreateCategory(Category category)
        {            
            var result = _categoryRepository.CreateCategory(category);
            if(result == null)
            {
                throw new CategoryNotCreatedException("This category id already exists");
            }

            return result;
        }

        public bool DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.DeleteCategory(categoryId);
            if (!result)
            {
                throw new CategoryNotFoundException("This category id not found");
            }

            return result;
        }

        public List<Category> GetAllCategoriesByUserId(string userId)
        {
            return _categoryRepository.GetAllCategoriesByUserId(userId);
        }

        public Category GetCategoryById(int categoryId)
        {
            var result = _categoryRepository.GetCategoryById(categoryId);
            if (result == null)
            {
                throw new CategoryNotFoundException("This category id not found");
            }

            return result;
        }

        public bool UpdateCategory(int categoryId, Category category)
        {
            var result = _categoryRepository.UpdateCategory(categoryId, category);
            if (!result)
            {
                throw new CategoryNotFoundException("This category id not found");
            }

            return result;
        }

        public int GetMaxCategoryId()
        {
            return _categoryRepository.GetMaxCategoryId();
        }
    }
}
