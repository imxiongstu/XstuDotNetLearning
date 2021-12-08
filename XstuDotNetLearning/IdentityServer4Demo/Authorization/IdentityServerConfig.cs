using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer4Demo.Authorization
{
    public class IdentityServerConfig
    {
        /// <summary>
        /// 定义资源范围
        /// </summary>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("api1", "我的第一个API")
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("api1")
            };
        }

        /// <summary>
        /// 定义访问的资源客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                 new Client
                 {
                     ClientId="client",//定义客户端ID
                     ClientSecrets=
                     {
                          new Secret("secret".Sha256())//定义客户端秘钥
                     },
                     AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//授权方式为用户密码模式授权，类型可参考GrantTypes枚举
                     AllowedScopes={ "api1"}//允许客户端访问的范围                      
                 }
            };
        }

        /// <summary>
        /// 这个方法是来规范tooken生成的规则和方法的。一般不进行设置，直接采用默认的即可。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                 new IdentityResources.OpenId()
            };
        }
    }
}
