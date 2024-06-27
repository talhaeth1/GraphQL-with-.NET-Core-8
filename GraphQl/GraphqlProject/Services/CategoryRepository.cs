using GraphqlProject.Data;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.Services
{
    public class CategoryRepository(GraphQLDbContext dbContext) : ICategoryRepository
    {
        private GraphQLDbContext dbContext = dbContext;
        public Category AddCategory(Category category)
        {
            ArgumentNullException.ThrowIfNull(category);
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            /*
             * var category = dbContext.Categories.Find(id)
             * if(category == null){throw new InvalidOperationException($"Category with Id {id} doesn't exist.");}
             */
            var category = dbContext.Categories.Find(id) 
                ?? 
                throw new InvalidOperationException($"Category with Id {id} doesn't exist.");
            dbContext.Categories.Remove(category);
            dbContext.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return dbContext.Categories.ToList();
        }

        public Category GetCategoryById(int id) 
        {
            return dbContext.Categories.FirstOrDefault(x => x.Id == id)
                ??
                throw new InvalidOperationException($"Category with Id {id} doesn't exist");
        }

        public List<Category> GetFilteredCategory(int? minId, int? maxId)
        {
            var query = dbContext.Categories
                 .AsQueryable();
        
            query = minId.HasValue ? query.Where(e => e.Id >= minId.Value) : query;
            query = maxId.HasValue ? query.Where(e => e.Id <= maxId.Value) : query;
            return query.ToList();
        }

        public Category UpdateCategory(int categoryId, Category category)
        {
            ArgumentNullException.ThrowIfNull(category);

            if (dbContext.Categories.Any(x => x.Id == categoryId))
            {
                var categoryResult = dbContext.Categories.Find(categoryId);
                categoryResult.Name = category.Name;
                categoryResult.Menus = category.Menus;
                categoryResult.ImageUrl = category.ImageUrl;
            }

            dbContext.SaveChanges();
            return category;
        }

       
    }
}
