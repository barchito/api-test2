namespace AdvancedStudioExercise.Infrastructure.Common {
    public interface IDependencyResolver {
        T Resolve<T> ();
    }
}