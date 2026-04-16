using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Data;
using PesquisaEleitoral.Extensions;
using PesquisaEleitoral.Repositories;
using PesquisaEleitoral.Repositories.Interfaces;
using PesquisaEleitoral.Service;
using PesquisaEleitoral.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Ajustando serialização dos enums.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//acessa a string de conecxão do appsettings
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//registrar o DbContext com MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IIntencaoDeVotoService, IntencaoDeVotoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
