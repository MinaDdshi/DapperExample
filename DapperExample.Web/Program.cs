using DapperExample.DataAccess.Context;
using DapperExample.DataAccess.Repositories;
using GettingStarted;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Host.CreateDefaultBuilder(args)
         .ConfigureServices((hostContext, services) =>
         {
             services.AddMassTransit(x =>
             {
                 // elided...

                 x.UsingRabbitMq((context, cfg) =>
                 {
                     cfg.Host("localhost", "/", h =>
                     {
                         h.Username("guest");
                         h.Password("guest");
                     });

                     cfg.ConfigureEndpoints(context);
                 });
             });

             services.AddHostedService<Worker>();
         });

IConfiguration _configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//builder.Services.AddDbContext<SqlDataAccess>(options =>
//{
//    options.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionStrings"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
