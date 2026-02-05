using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZapTask.Application.Commands.CriarTarefa;
using ZapTask.Application.Interfaces;
using ZapTask.Application.UseCases;
using ZapTask.Infrastructure.Database;
using ZapTask.Infrastructure.Jobs;
using ZapTask.Infrastructure.Repositories;
using ZapTask.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ZapTaskDbContext>(options =>
    options.UseSqlite(connection));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<CriarTarefaUseCase>();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
// builder.Services.AddHostedService<MotorDemandaServices>();
builder.Services.AddScoped<ConcluirTarefaUseCase>();
builder.Services.AddScoped<CriarTarefaCommandHandler>();


// builder.Services.AddHttpClient<WhatsAppService>(client =>
// {
//     // base address é opcional, pois já estamos usando URL completa
// });

// builder.Services.AddSingleton(provider =>
// {
//     var http = provider.GetRequiredService<HttpClient>();

//     // Pegando os valores da configuração e garantindo que não sejam nulos
//     var phoneNumberId = builder.Configuration["WhatsApp:PhoneNumberId"] 
//                         ?? throw new ArgumentNullException("WhatsApp:PhoneNumberId não configurado");

//     var accessToken = builder.Configuration["WhatsApp:AccessToken"] 
//                         ?? throw new ArgumentNullException("WhatsApp:AccessToken não configurado");

//     // Retornando a instância única
//     return new WhatsAppService(http, phoneNumberId, accessToken);
// });


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
