using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;

namespace AdvancedStudioExercise.Infrastructure.Repositories {
    public interface IIdentifiersRepository {
        Task<Guid> Add ( IdentifierModel identifier );
        Task Delete ( Guid identifierId );
    }
}