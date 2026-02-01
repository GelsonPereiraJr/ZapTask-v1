using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZapTask.Application.Interfaces;
using ZapTask.Application.UseCases;
using ZapTask.Infrastructure.Database;
using ZapTask.Infrastructure.Jobs;
using ZapTask.Infrastructure.Jobs.ZapTask.Infrastructure.Jobs;
using ZapTask.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ZapTaskDbContext>(options =>
    options.UseSqlite(connection));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<CriarTarefaUseCase>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddHostedService<MotorDemandaServices>();
builder.Services.AddScoped<ConcluirTarefaUseCase>();

var app = builder.Build();

// 🔹 Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => "API Boa Noite Lucas!!!!!!");

app.Run();
