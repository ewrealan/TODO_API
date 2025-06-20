using Microsoft.EntityFrameworkCore;
using Todo_Api.Data;
using Todo_Api.Models;

namespace Todo_Api.Services
{
    public class CategoryService
    {
        private readonly TodoContext _context;

        public CategoryService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category updatedCategory)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.Name = updatedCategory.Name;
            category.Color = updatedCategory.Color;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
