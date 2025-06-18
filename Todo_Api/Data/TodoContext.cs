using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using Todo_Api.Models;

namespace Todo_Api.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "İş", Color = "#0000FF", IconName = "work", IsDefault = true },
                new Category { Id = 2, Name = "Kişisel", Color = "#00FF00", IconName = "person", IsDefault = true },
                new Category { Id = 3, Name = "Alışveriş", Color = "#FFA500", IconName = "shopping", IsDefault = true }
            );
        }
    }
}
