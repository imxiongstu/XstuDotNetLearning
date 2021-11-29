using EFCoreMultipleDbContext.EntityFrameworkCore;
using EFCoreMultipleDbContext.Repository;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EFCoreMultipleDbContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
{
    builder.RegisterModule<AutofacServiceModule>();
}));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//注入数据上下文
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
