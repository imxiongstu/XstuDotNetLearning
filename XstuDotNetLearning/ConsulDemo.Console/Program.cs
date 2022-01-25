using (var consul = new Consul.ConsulClient(o => { o.Address = new Uri("http://127.0.0.1:8500"); }))
{
    // 获取所有服务
    var services = consul.Agent.Services().Result.Response;
    foreach (var service in services.Values)
    {
        using (var httpClient = new HttpClient())
        {
            var result = httpClient.GetAsync($"http://{service.Address}:{service.Port}/api/Health").Result;
            Console.WriteLine(result);
        }
    }
}
Console.ReadKey();