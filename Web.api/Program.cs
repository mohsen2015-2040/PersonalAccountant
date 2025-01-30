using Domain;
using Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Contract;
using Services.Implementation;
using System.Text;
using Web.Common;

var builder = WebApplication.CreateBuilder(args);


var connectionStr = builder.Configuration.GetConnectionString("ConnectionStr");

builder.Services.AddDbContext<DatabaseContext>(x =>
{
    x.UseSqlServer(connectionStr);
    x.UseLazyLoadingProxies();
});

builder.Services.AddScoped<ExceptionHandler>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ExceptionHandler>();
});

builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});

builder.Services.AddDataProtection();

builder.Services.AddSingleton<IDataProtecting, DataProtecting>();
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddScoped<IValidator<Web.api.Endpoints.Account.CreateControllerModel>, Web.api.Endpoints.Account.CreateControllerModelValidation>();
builder.Services.AddScoped<IValidator<Web.api.Endpoints.Account.ChangePasswordControllerModel>, Web.api.Endpoints.Account.ChangePasswordControllerModelValidation>();
builder.Services.AddScoped<IValidator<Web.api.Endpoints.Account.LoginControllerModel>, Web.api.Endpoints.Account.LoginControllerModelValidation>();
builder.Services.AddScoped<IValidator<Web.api.Endpoints.Transaction.CreateControllerModel>, Web.api.Endpoints.Transaction.CreateControllerModelValidation>();
builder.Services.AddScoped<IValidator<Web.api.Endpoints.Transaction.EditControllerModel>, Web.api.Endpoints.Transaction.EditControllerModelValidation>();
builder.Services.AddScoped<IValidator<Web.api.Endpoints.CreditCard.CreateControllerModel>, Web.api.Endpoints.CreditCard.CreateControllerModelValidation>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
        .AllowAnyMethod().AllowAnyHeader();
    });
});

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtAudience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtAudience,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

var app = builder.Build();

app.UseCors("_myAllowOrigin");
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
