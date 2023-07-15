using AutoMapper;
using FirebaseAdmin;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using UsersApi;
using UsersApi.Authorizations.AuthorizationHandlers;
using UsersApi.Authorizations.AuthorizationRequirements;
using UsersApi.Configs;
using UsersApi.ExternalAPIs;
using UsersApi.FluentValidation;
using UsersApi.Interfaces;
using UsersApi.MapperConfig;
using UsersApi.Models.Request;

var builder = WebApplication.CreateBuilder(args);

//TODO - use real time
//Use settings
var tokenTimeout = 5;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IReqResApi, ReqResApi>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(origin =>
        new Uri(origin).Host == "localhost" ||
        new Uri(origin).Host == "facebook.com" ||
        new Uri(origin).Host == "google.com"
        );
    });
});



builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))
}));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TokenPolicy", policy =>
        policy.Requirements.Add(new TokenRequirement(tokenTimeout)));
});
builder.Services.AddSingleton<IAuthorizationHandler, TokenAuthorizationHandler>();


builder.Services.AddFirebaseAuthentication();

builder.Services.AddTransient<IValidator<CreateUserRequestModel>, CreateUserRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateUserRequestModel>, UpdateUserRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionsHandler>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", (ClaimsPrincipal user) =>
{
    return "Hello from UsersApi";
});

app.Run();
