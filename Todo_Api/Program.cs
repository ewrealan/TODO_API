using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Todo_Api.Data;
using Todo_Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<CategoryService>();

// 🌐 CORS - Angular erişimi için
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🔁 JSON döngüsel referans engeli
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Angular CORS için aktif hale getir
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();
app.Run();
