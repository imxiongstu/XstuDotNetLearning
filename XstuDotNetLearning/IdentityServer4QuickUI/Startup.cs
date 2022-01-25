using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public class ClientInitConfig
    {
        /// <summary>
        /// 定义ApiResource   
        /// 这里的资源（Resources）指的就是管理的API
        /// </summary>
        /// <returns>多个ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "用户获取API")
            };
        }

        /// <summary>
        /// 定义验证条件的Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "AuthDemo",//客户端惟一标识
                    ClientSecrets = new [] { new Secret("zhangsan123456".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new [] { "UserApi" },//允许访问的资源
                    //3.1和4.1对Claims的名称不同 3.1是Claim  4.1是ClientClaim
                    Claims=new List<ClientClaim>(){
                        new ClientClaim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new ClientClaim(IdentityModel.JwtClaimTypes.NickName,"zhangsan"),
                        new ClientClaim("eMail","123456@qq.com")
                    }
                }
            };
        }
    }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddControllers();
        services.AddControllersWithViews();
        #region 客户端
        services.AddIdentityServer()
          .AddDeveloperSigningCredential()//默认的开发者证书 
          .AddInMemoryClients(ClientInitConfig.GetClients())
          .AddInMemoryApiResources(ClientInitConfig.GetApiResources());
        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        //访问wwwroot目录静态文件
        app.UseStaticFiles(
            new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

        #region 添加IdentityServer中间件
        app.UseIdentityServer();
        #endregion
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}