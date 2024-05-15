using Microsoft.OpenApi.Models;
using Persistence.Auth.Jwt;
using IOC;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ApplicationFormContext>(options =>
//    options.UseCosmos(
//));

builder.Services.AddCors(c => c
                .AddPolicy("CorsPolicy", builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Capital_Placement_Application_Form", Version = "v1" });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddTransient<GlobalExceptionMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services?.GetService<IServiceScopeFactory>()?.CreateAsyncScope();
    var context = scope?.ServiceProvider.GetRequiredService<ApplicationFormContext>();
    var result = await context!.Database.EnsureCreatedAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalException();

app.MapControllers();

app.Run();
