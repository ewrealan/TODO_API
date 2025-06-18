using Microsoft.EntityFrameworkCore;
using Todo_Api.Data;
using Todo_Api.Dtos;
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

        // DTO ile dönen versiyon
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Color = c.Color
                })
                .ToListAsync();
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Color = c.Color
                })
                .FirstOrDefaultAsync();
        }

        // Geri kalanlar şimdilik aynı kalabilir:
        public async Task<Category> CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateAsync(int id, Category updatedCategory)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null) return false;

            existing.Name = updatedCategory.Name;
            existing.Color = updatedCategory.Color;
            existing.IconName = updatedCategory.IconName;
            existing.IsDefault = updatedCategory.IsDefault;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
