using IdentityServer4Demo.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()//注册服务
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())//配置类定义的授权范围
                    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())//配置类定义的授权范围
                    .AddInMemoryClients(IdentityServerConfig.GetClients())//配置类定义的授权客户端
                    .AddTestUsers(new List<IdentityServer4.Test.TestUser>()//添加测试用户
                    {
                        new IdentityServer4.Test.TestUser()
                        {
                            Username="admin",
                            Password="123456",
                            SubjectId="10001",
                            IsActive=true
                        }
                    });

            //使用授权服务
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000";//鉴权中心地址
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "api1";//鉴权范围
                    });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer4Demo", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer4Demo v1"));
            }

            app.UseIdentityServer();//添加中间件

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
