using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacDemo;

var builder = WebApplication.CreateBuilder(args);
//替换默认的ServiceProviderFactory为Atufac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
{
    //注册Autofac模块（其实也可以直接在这里写依赖注入的东西，只不过单独写在一个服务模块里面比较好）
    builder.RegisterModule(new AutofacServiceModule());
}));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();