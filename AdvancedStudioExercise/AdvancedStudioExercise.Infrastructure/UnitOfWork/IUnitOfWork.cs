using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Infrastructure.Repositories;

namespace AdvancedStudioExercise.Infrastructure.UnitOfWork {
    public interface IUnitOfWork : IDisposable {
        IPersonsRepository PersonsRepository { get; }
        IIdentifiersRepository IdentifiersRepository { get; }
        void SaveChanges ();
        Task SaveChangesAsync ();
    }
}