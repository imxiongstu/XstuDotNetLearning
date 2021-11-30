using Autofac;
using EFCoreMultipleDbContext.Repository;
namespace EFCoreMultipleDbContext
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
        }
    }
}
