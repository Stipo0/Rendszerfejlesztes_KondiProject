using KondiProject.API.Data;
using KondiProject.API.Enums;
using KondiProject.API.Helpers.FileService;
using KondiProject.API.Helpers.HttpAccesor;
using KondiProject.API.Options;
using KondiProject.API.Repositories.GymMachineRepository;
using KondiProject.API.Repositories.GymRepository;
using KondiProject.API.Repositories.UserRepository;
using KondiProject.API.Services.GymMachineService;
using KondiProject.API.Services.GymService;
using KondiProject.API.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection(nameof(ApplicationDbContext)).Value);
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
      policy.RequireRole(nameof(Role.Admin)));

    options.AddPolicy("GymPolicy", policy =>
      policy.RequireRole(nameof(Role.Gym)));

    options.AddPolicy("UserPolicy", policy =>
          policy.RequireRole(nameof(Role.Admin), nameof(Role.User)));
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:Key").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();

//Dependencies
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGymService, GymService>();
builder.Services.AddScoped<IGymRepository,GymRepository>();
builder.Services.AddScoped<IGymMachineService,GymMachineService>();
builder.Services.AddScoped<IGymMachineRepository, GymMachineRepository>();
builder.Services.AddScoped<IHttpAccessor, HttpAccessor>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapControllers();

app.Run();
