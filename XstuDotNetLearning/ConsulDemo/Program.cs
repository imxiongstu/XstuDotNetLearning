using Consul;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
 *  配置Consul
 */
var url = new Uri(builder.Configuration["Urls"]);
//当前服务会运行的IP
string ip = url.Host;
// 当前服务会运行的端口
int port = url.Port;
// 服务名称
string serviceName = "MsgService";
// 服务ID
string serviceId = serviceName + Guid.NewGuid();

// 注册Consul
using (var consulClient = new ConsulClient(o =>
{
    // Consul的地址
    o.Address = new Uri("http://127.0.0.1:8500");
    // 数据中心
    o.Datacenter = "dc1";
}))
{
    // 向Consul注册服务
    AgentServiceRegistration asr = new AgentServiceRegistration()
    {
        Address = ip,
        Port = port,
        ID = serviceId,
        Name = serviceName,
        // 配置服务检查（会按照配置去轮询这个接口，来检测服务的状态）
        Check = new AgentServiceCheck()
        {
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
            HTTP = $"http://{ip}:{port}/api/Health",
            Interval = TimeSpan.FromSeconds(10),
            Timeout = TimeSpan.FromSeconds(5)
        }
    };
    consulClient.Agent.ServiceRegister(asr).Wait();
};

// 注销Consul
app.Lifetime.ApplicationStopped.Register(() =>
{
    using (var consulClient = new ConsulClient(o =>
      {
          o.Address = new Uri("http://127.0.0.1:8500");
          o.Datacenter = "dc1";
      }))
    {
        Console.WriteLine("应用退出，开始从Consul注销");
        consulClient.Agent.ServiceDeregister(serviceId).Wait();
    }
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
