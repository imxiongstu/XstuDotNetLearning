using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacDemo;

var builder = WebApplication.CreateBuilder(args);
//�滻Ĭ�ϵ�ServiceProviderFactoryΪAtufac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
{
    //ע��Autofacģ�飨��ʵҲ����ֱ��������д����ע��Ķ�����ֻ��������д��һ������ģ������ȽϺã�
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