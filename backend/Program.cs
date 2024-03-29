using AutoMapper;
using backend.Context;
using backend.DTOs.Mapping;
using backend.Exceptions;
using backend.Middlewares;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;
using backend.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers(options =>
{
    //Filter Add ErroServiceException
    options.Filters.Add<ErrorHandlingFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection My Database
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(mySqlConnection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//Configuration Jwt

IConfiguration configuration = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration.GetSection("Jwt")["issuer"],
        ValidAudience = builder.Configuration.GetSection("Jwt")["audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["secretKey"]))
    };
});

//Configuration validator


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<EmployeeDTOCreateValidation>())
    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<EmployeeDTOValidation>())
    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<EmployeeMarkingDTOValidation>());


//Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeMarkingRepository, EmployeeMarkingRepository>();

//Config AutoMaper
builder.Services.AddAutoMapper(typeof(MappingDTOToModel));

//Service My Application
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeMarkingService, EmployeeMarkingService>();
builder.Services.AddScoped<IMarkingService, MarkingService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<Error500Middleware>();
}

//app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin()
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
