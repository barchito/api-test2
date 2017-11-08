using AdvancedStudioExercise.Domain.Services;
using AdvancedStudioExercise.Domain.Services.General;
using AdvancedStudioExercise.Infrastructure.Common;
using Autofac;

namespace AdvancedStudioExercise.IoC {
    public class DomainModule : Module {
        protected override void Load ( ContainerBuilder builder ) {
            builder.RegisterType<ContainerManager>().As<IDependencyResolver>();

            #region Services registration

            builder.RegisterType<PersonService>().As<IPersonService>();
            builder.RegisterType<IdentifierService>().As<IIdentifierService>();

            #endregion

            base.Load( builder );
        }
    }
}