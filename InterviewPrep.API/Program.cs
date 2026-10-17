using InterviewPrep.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using InterviewPrep.Application.Interfaces;
using InterviewPrep.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter JWT token"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
}); builder.Services.AddDbContext<InterviewPrepDbContext>(options =>
{
    options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<
    ISearchService,
    SearchService>();
var key = Encoding.UTF8.GetBytes(
    builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(key)
        };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
    policy =>
    {
        policy.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


