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

        // ❗ ToLowerInvariant() yerine client-side filtreleme yapıldı (EF Core uyumsuzluk çözümü)
        public Task<Category?> GetByNameAsync(string name)
        {
            var result = _context.Categories
                .AsEnumerable()
                .FirstOrDefault(c =>
                    string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(result);
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

        // ✅ Varsayılan kategorileri bir kereye mahsus ekle
        public async Task SeedAsync()
        {
            if (!_context.Categories.Any())
            {
                var defaultCategories = new List<Category>
                {
                    new() { Name = "İş", Color = "#3498db" },
                    new() { Name = "Kişisel", Color = "#e67e22" },
                    new() { Name = "Alışveriş", Color = "#9b59b6" },
                    new() { Name = "Sağlık", Color = "#2ecc71" },
                    new() { Name = "Eğitim", Color = "#e74c3c" }
                };

                _context.Categories.AddRange(defaultCategories);
                await _context.SaveChangesAsync();
            }
        }
    }
}
