using API.Endpoints;
using API.Persistence.Context;
using API.Persistence.Repository.Implementation;
using API.Persistence.Repository.Interface;
using API.Persistence.UnitOfWork;
using API.Services.Implementation;
using API.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Allows all origins
              .AllowAnyMethod()  // Allows all HTTP methods
              .AllowAnyHeader(); // Allows all headers
    });
});

// Add repositories to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITagRepo, TagRepo>();
builder.Services.AddScoped<IVisualizationRepo, VisualizationRepo>();
builder.Services.AddScoped<IAlgorithmRepo, AlgorithmRepo>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add services to the container.
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAlgorithmService, AlgorithmService>();
builder.Services.AddScoped<IVisualizationService, VisualizationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TokenService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=database.db"));



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
    jwtOptions.TokenValidationParameters = TokenService.GetTokenValidationParameter(builder.Configuration));

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapEndPoints();

app.Run();

