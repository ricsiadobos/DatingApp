using API.Data;
using API.Interfaces;
using API.Servives;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Ez kell, hogy serverként működjön
builder.Services.AddCors();

//Token kezelésre létrehozott service 
builder.Services.AddScoped<ITokenService, TokenSevice>();

//itt adjuk �t a db forrást a rendszerben
builder.Services.AddDbContext<DataContext>(
    options =>options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

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

//Ez kell, hogy egy másik rendszer middleware-nek használja
app.UseCors(x => x
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("https://localhost:4200"));

app.UseAuthorization();

app.MapControllers();

app.Run();
