using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Todo_Api.Data;
using Todo_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisler
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<CategoryService>();

// 🌐 CORS - Angular uygulamanızın IP:Port’una izin veriyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
          .WithOrigins(
            "http://localhost:4200",
            "http://10.6.40.104:4200"
          )
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

// 🔁 JSON döngüsel referans engelleme
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Kategori seed işlemi yapılacak
using (var scope = app.Services.CreateScope())
{
    var categoryService = scope.ServiceProvider.GetRequiredService<CategoryService>();
    await categoryService.SeedAsync(); // 👈 otomatik veri eklemesi burada
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run("http://10.6.40.104:5145");
