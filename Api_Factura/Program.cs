using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configura para manejar referencias cíclicas en la serialización JSON
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Configura DbContext para usar SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Omite la configuración de CORS
 builder.Services.AddCors(options =>
 {


     options.AddDefaultPolicy(police =>
     {
         police.AllowAnyOrigin()  // Permite cualquier origen
                  .AllowAnyHeader()
                 .AllowAnyMethod();
     });


 });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Omite UseCors
// app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
