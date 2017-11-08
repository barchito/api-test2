using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;

namespace AdvancedStudioExercise.Domain.Services {
    public interface IIdentifierService {
        Task<Guid> AddIdentifierToPerson ( IdentifierModel identifier );
        Task<bool> DeleteIdentifier ( Guid identifierId );
    }
}