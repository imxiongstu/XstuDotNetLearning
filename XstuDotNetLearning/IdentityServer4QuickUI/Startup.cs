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
        /// ����ApiResource   
        /// �������Դ��Resources��ָ�ľ��ǹ����API
        /// </summary>
        /// <returns>���ApiResource</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("UserApi", "�û���ȡAPI")
            };
        }

        /// <summary>
        /// ������֤������Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "AuthDemo",//�ͻ���Ωһ��ʶ
                    ClientSecrets = new [] { new Secret("zhangsan123456".Sha256()) },//�ͻ������룬�����˼���
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //��Ȩ��ʽ���ͻ�����֤��ֻҪClientId+ClientSecrets
                    AllowedScopes = new [] { "UserApi" },//������ʵ���Դ
                    //3.1��4.1��Claims�����Ʋ�ͬ 3.1��Claim  4.1��ClientClaim
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
        #region �ͻ���
        services.AddIdentityServer()
          .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
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
        //����wwwrootĿ¼��̬�ļ�
        app.UseStaticFiles(
            new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

        #region ���IdentityServer�м��
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