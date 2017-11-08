using AdvancedStudioExercise.Domain.Services.Base;
using AdvancedStudioExercise.Infrastructure.Common;

namespace AdvancedStudioExercise.Domain.Services.General.Base {
    public class ServiceBase : IServiceBase {
        protected readonly IDependencyResolver DependencyResolver;

        public ServiceBase ( IDependencyResolver dependencyResolver ) {
            DependencyResolver = dependencyResolver;
        }
    }
}