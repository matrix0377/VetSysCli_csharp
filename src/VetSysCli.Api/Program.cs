using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Npgsql;
using VetSysCli.Application.Interfaces;
using VetSysCli.Application.Services;
using VetSysCli.Core.Entities;
using VetSysCli.Infrastructure.Data;
using VetSysCli.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=postgres;Port=5432;Database=vetsysdb;Username=vetsys;Password=102550";

builder.Services.AddDbContext<VetSysDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
var uploadsPath = Path.Combine(app.Environment.ContentRootPath, "uploads");
Directory.CreateDirectory(uploadsPath);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VetSysDbContext>();

    for (var attempt = 1; attempt <= 60; attempt++)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            break;
        }
        catch (NpgsqlException)
        {
            if (attempt == 60)
            {
                throw;
            }

            Thread.Sleep(2000);
        }
    }

    db.Database.EnsureCreated();

    if (!db.Animals.Any())
    {
        db.Animals.AddRange(
            new Animal { Id = Guid.NewGuid(), Name = "Luna", Type = "Cachorro", Breed = "Vira-lata", BirthDate = DateTime.UtcNow.AddYears(-3), PhotoPath = "/uploads/default.png" },
            new Animal { Id = Guid.NewGuid(), Name = "Rex", Type = "Cachorro", Breed = "Poodle", BirthDate = DateTime.UtcNow.AddYears(-5), PhotoPath = "/uploads/default.png" });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
