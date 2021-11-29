using EFCoreMultipleDbContext.EntityFrameworkCore;
using EFCoreMultipleDbContext.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ע������������
builder.Services.AddDbContext<MainDbContext>(options =>
{
    options.UseMySql(configuration.GetConnectionString("MySqlConnectionString"), ServerVersion.Parse("5.7"));
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
