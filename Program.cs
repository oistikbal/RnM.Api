using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RnM.Api.DB;
using RnM.Api.Models;
using RnM.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RnMContext>(options =>
    options.UseSqlite($"Data Source={builder.Configuration.GetValue<string>("Database")}.sqlite"), ServiceLifetime.Scoped) ;
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
    DataSeeder.Seed(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
