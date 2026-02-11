using BLL;
using System.Text;
using API.Middleware.Exceptions;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Context;
using DAL.Helpers.Search;
using DAL.Helpers.Sorting;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the Bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Database
var connectionString = builder.Configuration.GetConnectionString("PgsqlConnection");
builder.Services.AddDbContext<WardrobeDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
});

// Exception Handling
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//Fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Repositories + UoW
builder.Services.AddScoped(typeof(ISortHelper<>), typeof(SortHelper<>));
builder.Services.AddScoped(typeof(ISearchHelper<>), typeof(SearchHelper<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IClothingItemRepository, ClothingItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<ITemperatureSuitabilityRepository, TemperatureSuitabilityRepository>();
builder.Services.AddScoped<IStyleRepository, StyleRepository>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IOutfitRepository, OutfitRepository>();
builder.Services.AddScoped<IOutfitGroupRepository, OutfitGroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IClothingItemService, ClothingItemService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<ITemperatureSuitabilityService, TemperatureSuitabilityService>();
builder.Services.AddScoped<IStyleService, StyleService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IOutfitService, OutfitService>();
builder.Services.AddScoped<IOutfitGroupService, OutfitGroupService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container
var key = Encoding.UTF8.GetBytes("W8zDp4x2mY9vK6nF3qR7tW5eX2aZ7pU6sQ9bJ4vL2cT8nR5oX3kV6rP7mY2qJ9");
builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WardrobeDbContext>();
    await dbContext.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactLocalhost");

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();