using Autofac;
using EFCoreMultipleDbContext.Repository;
namespace EFCoreMultipleDbContext
{
    public class AutofacServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}
