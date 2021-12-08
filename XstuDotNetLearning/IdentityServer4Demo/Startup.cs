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
            services.AddIdentityServer()//ע�����
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())//�����ඨ�����Ȩ��Χ
                    .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())//�����ඨ�����Ȩ��Χ
                    .AddInMemoryClients(IdentityServerConfig.GetClients())//�����ඨ�����Ȩ�ͻ���
                    .AddTestUsers(new List<IdentityServer4.Test.TestUser>()//��Ӳ����û�
                    {
                        new IdentityServer4.Test.TestUser()
                        {
                            Username="admin",
                            Password="123456",
                            SubjectId="10001",
                            IsActive=true
                        }
                    });

            //ʹ����Ȩ����
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:5000";//��Ȩ���ĵ�ַ
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "api1";//��Ȩ��Χ
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

            app.UseIdentityServer();//����м��

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
