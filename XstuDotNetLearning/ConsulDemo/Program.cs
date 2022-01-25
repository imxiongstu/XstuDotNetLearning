using Consul;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
 *  ����Consul
 */
var url = new Uri(builder.Configuration["Urls"]);
//��ǰ��������е�IP
string ip = url.Host;
// ��ǰ��������еĶ˿�
int port = url.Port;
// ��������
string serviceName = "MsgService";
// ����ID
string serviceId = serviceName + Guid.NewGuid();

// ע��Consul
using (var consulClient = new ConsulClient(o =>
{
    // Consul�ĵ�ַ
    o.Address = new Uri("http://127.0.0.1:8500");
    // ��������
    o.Datacenter = "dc1";
}))
{
    // ��Consulע�����
    AgentServiceRegistration asr = new AgentServiceRegistration()
    {
        Address = ip,
        Port = port,
        ID = serviceId,
        Name = serviceName,
        // ���÷����飨�ᰴ������ȥ��ѯ����ӿڣ����������״̬��
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

// ע��Consul
app.Lifetime.ApplicationStopped.Register(() =>
{
    using (var consulClient = new ConsulClient(o =>
      {
          o.Address = new Uri("http://127.0.0.1:8500");
          o.Datacenter = "dc1";
      }))
    {
        Console.WriteLine("Ӧ���˳�����ʼ��Consulע��");
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
