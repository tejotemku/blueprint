using Microsoft.AspNetCore.Diagnostics;
using BlueprintBackend;
using BlueprintBackend.Exceptions;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Controllers;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .AddEnvironmentVariables()
        .Build();


var db = new PostgresDBmanager(config);


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
builder.Services.AddControllers();
builder.Services.AddSingleton(new BlueprintService());
builder.Services.AddScoped<IBlueprintService, BlueprintService>();
builder.Services.AddScoped<IDataBase, PostgresDBmanager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
