using Microsoft.EntityFrameworkCore;
using TestApiProject.Data;
using TestApiProject.Repositories;
using TestApiProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TestDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CharacterRepository>();
builder.Services.AddScoped<SuperPowerRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CharacterService>();
builder.Services.AddScoped<SuperPowerService>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
