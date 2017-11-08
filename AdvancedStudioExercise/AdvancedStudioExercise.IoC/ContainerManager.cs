using AdvancedStudioExercise.Infrastructure.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedStudioExercise.IoC {
    public class ContainerManager : IDependencyResolver {
        private static IContainer _container;

        public static IContainer BuildContainer ( IServiceCollection serviceCollection = null ) {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule( new InfrastructureModule() );
            containerBuilder.RegisterModule( new DomainModule() );
            //containerBuilder.RegisterModule(new AutoMapperModule());           

            if ( serviceCollection != null ) {
                containerBuilder.Populate( serviceCollection );
            }

            _container = containerBuilder.Build();

            return _container;
        }

        T IDependencyResolver.Resolve<T> () {
            return _container.Resolve<T>();
        }
    }
}