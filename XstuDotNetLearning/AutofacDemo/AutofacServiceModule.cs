using Autofac;
using AutofacDemo.Services;

namespace AutofacDemo
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().As<IPersonService>();
        }
    }
}
