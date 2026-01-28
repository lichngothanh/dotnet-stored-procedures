using Api.Extensions;
using Api.Middlewares;
using Application.Interfaces;
using Application.Mapping;
using Domain.SuperHeroes;
using Infrastructure.Ado.Persistence;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Configurations;
using Infrastructure.Shared.Implementations;
using Infrastructure.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(SuperHeroProfile).Assembly
);
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection(DatabaseOptions.SectionName));

// Register Repository layer
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();

// Register Service layer
builder.Services.AddScoped<ISuperHeroService, SuperHeroService>();

// Register SqlExecutor helper
builder.Services.AddScoped<ISqlExecutor, SqlExecutor>();

// Add ModelState validation filter
builder.Services.AddModelStateConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandle>();
app.MapControllers();

app.Run();
