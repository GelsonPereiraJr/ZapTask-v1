using Scalar.AspNetCore;
using ZapTask.Application.Interfaces;
using ZapTask.Application.UseCases;
using ZapTask.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<CriarTarefaUseCase>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

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
