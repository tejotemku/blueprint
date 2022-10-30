﻿using Microsoft.AspNetCore.Diagnostics;
using BlueprintBackend;
using BlueprintBackend.Exceptions;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Services;
using BlueprintBackend.Controllers;

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .AddEnvironmentVariables()
        .Build();


var MyAllowSpecificOrigins = "localhostPolicy";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler(options =>
{
    options.AllowStatusCode404Response = true;
    options.ExceptionHandler = async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        context.Response.StatusCode = contextFeature?.Error switch
        {
            EntityNotFoundException => 404,
            EntityDuplicateException => 422,
            BadRequestException => 400,
            _ => 500
        };
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
/*            policy.WithOrigins("http://localhost:8080",
                                "https://localhost:8080");*/
        });
});
builder.Services.AddControllers();
builder.Services.AddSingleton(new PostgresDBmanager(config));
builder.Services.AddScoped<IDataBase, PostgresDBmanager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();