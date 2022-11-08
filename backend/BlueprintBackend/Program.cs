using Microsoft.AspNetCore.Diagnostics;
using BlueprintBackend.Exceptions;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Services;
using BlueprintBackend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true)
        .AddEnvironmentVariables()
        .Build();
var MyAllowSpecificOrigins = "localhostPolicy";
string secret = config.GetValue<string>("JWT_SECRET");
var key = Encoding.UTF8.GetBytes(secret);


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
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddControllers();
builder.Services.AddScoped<IDataBase, PostgresDBService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSingleton(new BlueprintUtils(config, new PostgresDBService(config)));


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
