using Autofac;
using University.Core.Services;

namespace University.API.Modules
    {
    public class ServiceModule : Module
        {
        protected override void Load ( ContainerBuilder builder )
            {
            builder.RegisterAssemblyTypes (typeof (StudentService).Assembly)
                .Where (t => t.Name.EndsWith ("Service"))
                .AsImplementedInterfaces ()
                .InstancePerLifetimeScope ();
            }
        }
    }