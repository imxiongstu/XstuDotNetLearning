using Autofac;
using Autofac.Extras.DynamicProxy;
using InterceptorDemo.Services;

namespace InterceptorDemo
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestInterceptor>();
            builder.RegisterType<PersonService>().As<IPersonService>().EnableInterfaceInterceptors().InterceptedBy(typeof(RequestInterceptor));
        }
    }
}
