using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.Sql.Repositories;
using AdvancedStudioExercise.Infrastructure.Sql.UnitOfWork;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;
using Autofac;

namespace AdvancedStudioExercise.IoC {
    public class InfrastructureModule : Module {
        protected override void Load ( ContainerBuilder builder ) {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            #region Infrastructure dependencies registration

            builder.RegisterType<PersonsRepository>().As<IPersonsRepository>();

            #endregion

            base.Load( builder );
        }
    }
}