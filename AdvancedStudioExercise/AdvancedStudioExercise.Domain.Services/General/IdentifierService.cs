using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Exceptions;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Domain.Services.General.Base;
using AdvancedStudioExercise.Infrastructure.Common;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;

namespace AdvancedStudioExercise.Domain.Services.General {
    public class IdentifierService : ServiceBase, IIdentifierService {
        public IdentifierService ( IDependencyResolver dependencyResolver ) : base( dependencyResolver ) {
        }

        public async Task<Guid> AddIdentifierToPerson ( IdentifierModel identifier ) {
            using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                var personExists = await unitOfWork.PersonsRepository.Get( identifier.PersonId ) != null;
                if ( !personExists ) {
                    throw new PersonNotFoundException();
                }
                var id = await unitOfWork.IdentifiersRepository.Add( identifier );
                await unitOfWork.SaveChangesAsync();
                return id;
            }
        }

        public async Task<bool> DeleteIdentifier ( Guid identifierId ) {
            try {
                using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                    await unitOfWork.IdentifiersRepository.Delete( identifierId );
                    await unitOfWork.SaveChangesAsync();

                    return true;
                }
            } catch {
                return false;
            }
        }
    }
}