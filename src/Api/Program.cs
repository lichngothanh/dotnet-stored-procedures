using Api.Middlewares;
using Domain.SuperHeroes;
using Infrastructure.Ado.Persistence;
using Infrastructure.Shared.Abstractions;
using Infrastructure.Shared.Configurations;
using Infrastructure.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection(DatabaseOptions.SectionName));

builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();

// Register SqlExecutor helper
builder.Services.AddScoped<ISqlExecutor, SqlExecutor>();

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
