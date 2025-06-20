using Microsoft.AspNetCore.Mvc;
using Todo_Api.Models;
using Todo_Api.Services;
using Todo_Api.Dtos;

namespace Todo_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ✅ Tüm kategorileri getir
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();

            // Category → CategoryDto dönüşümü
            var dtoList = categories.Select(c => new CategoryDto
            {
                Name = c.Name,
                Color = c.Color
            });

            return Ok(dtoList);
        }

        // ✅ Belirli bir kategoriyi getir
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();

            var dto = new CategoryDto
            {
                Name = category.Name,
                Color = category.Color
            };

            return Ok(dto);
        }

        // ✅ Yeni kategori oluştur (CreateCategoryDto ile)
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Color = dto.Color,
                IsDefault = false
            };

            var created = await _categoryService.CreateAsync(category);

            var resultDto = new CategoryDto
            {
                Name = created.Name,
                Color = created.Color
            };

            return CreatedAtAction(nameof(GetCategory), new { id = created.Id }, resultDto);
        }

        // 🟡 İsteğe Bağlı: Güncelleme için UpdateCategoryDto eklenebilir
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            var success = await _categoryService.UpdateAsync(id, updatedCategory);
            if (!success) return NotFound();
            return NoContent();
        }

        // ✅ Kategori sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
