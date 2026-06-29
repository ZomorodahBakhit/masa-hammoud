using Autofac;
using University.Data.Repositories;

namespace University.API.Modules
    {
    public class RepositoryModule : Module
        {
        protected override void Load ( ContainerBuilder builder )
            {
            builder.RegisterAssemblyTypes (typeof (StudentRepository).Assembly)
                .Where (t => t.Name.EndsWith ("Repository"))
                .AsImplementedInterfaces ()
                .InstancePerLifetimeScope ();
            }
        }
    }