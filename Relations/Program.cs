using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Relations;
using Relations.Domain.Identity.DTO.JWT;
using Relations.Domain.Identity.Extensions;
using Relations.Infrastructure;
using Relations.Application.Extensions;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Relations.Domain.DTO.UserDto;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

await builder.Services.InitializeServices();
await builder.Services.InitializeRepositories();
await builder.Services.InitializeSwagger();
await builder.Services.InitializeMediator();

builder.Services.Configure<JwtTokenSettings>(
    builder.Configuration.GetSection(nameof(JwtTokenSettings)));

builder.Services.AddJwtAuthentication(
    builder.Configuration.GetValue<string>("JwtTokenSettings:JwtIssuer"),
    builder.Configuration.GetValue<string>("JwtTokenSettings:JwtAudience"),
    builder.Configuration.GetValue<string>("JwtTokenSettings:JwtKey"),
    builder.Configuration.GetValue<int>("JwtTokenSettings:JwtExpires")
);

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.CreateDefaultRoles();
await app.CreateDefaultUsers();

await app.RunAsync();
