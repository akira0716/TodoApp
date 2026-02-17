using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TodoApp.Application.Services;
using TodoApp.Application.Validators;
using TodoApp.Domain.Interfaces;
using TodoApp.Infrastructure.Data;
using TodoApp.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SQLite‚Ìİ’è‚ğ’Ç‰Á
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

// ƒŠƒ|ƒWƒgƒŠ‚Ì“o˜^
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Application Service‚Ì“o˜^
builder.Services.AddScoped<TodoService>();

// Validator‚Ì“o˜^
builder.Services.AddValidatorsFromAssemblyContaining<CreateTodoDtoValidator>();

// ƒnƒ“ƒhƒ‰[‚Ì“o˜^
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
