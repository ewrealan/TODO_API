using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Todo_Api.Data;
using Todo_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Veritabanı bağlantısı (SQL Server)
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔧 Servisleri DI container'a ekle
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<CategoryService>();

// 🌐 CORS ayarı (Angular için gerekli)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 🔁 JSON döngüsel referans hatasını engelle
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 📦 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 📄 Swagger arayüzü
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🌐 HTTPS + CORS
app.UseHttpsRedirection();
app.UseCors();

// 🔐 Yetkilendirme (şimdilik boş)
app.UseAuthorization();

// 📍 Controller yönlendirmeleri
app.MapControllers();

app.Run();
