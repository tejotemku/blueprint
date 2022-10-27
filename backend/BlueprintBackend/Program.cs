using Microsoft.AspNetCore.Diagnostics;
using BlueprintBackend;
using BlueprintBackend.Exceptions;
using BlueprintBackend.Interfaces;



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


var db = new PostgresDBmanager();

builder.Services.AddControllers();
builder.Services.AddSingleton(new BlueprintService());
builder.Services.AddScoped<IBlueprintService, BlueprintService>();
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
