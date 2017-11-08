using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Domain.Services.General.Base;
using AdvancedStudioExercise.Infrastructure.Common;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;

namespace AdvancedStudioExercise.Domain.Services.General {
    public class PersonService : ServiceBase, IPersonService {
        public PersonService ( IDependencyResolver dependencyResolver ) : base( dependencyResolver ) {
        }

        public async Task<IEnumerable<PersonModel>> GetAll () {
            using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                return await unitOfWork.PersonsRepository.Get();
            }
        }

        public async Task<IEnumerable<PersonModel>> GetByFirstNameOrLastName ( string firstNameOrLastName ) {
            using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                var persons = await unitOfWork.PersonsRepository.GetByFirstNameOrLastName( firstNameOrLastName );
                return persons;
            }
        }

        public async Task<IEnumerable<PersonModel>> GetWithSpesificIdentifiers ( IEnumerable<Guid> identifiers ) {
            using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                var persons = await unitOfWork.PersonsRepository.GetWithSpesificIdentifiers( identifiers );
                return persons;
            }
        }

        public async Task<Guid> Create ( PersonModel person ) {
            using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                var id = await unitOfWork.PersonsRepository.Add( person );
                await unitOfWork.SaveChangesAsync();
                return id;
            }
        }

        public async Task<bool> DeletePerson ( Guid personId ) {
            try {
                using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                    await unitOfWork.PersonsRepository.Delete( personId );
                    await unitOfWork.SaveChangesAsync();
                    return true;
                }
            } catch {
                return false;
            }
        }

        public async Task<bool> UpdatePerson ( PersonModel person ) {
            try {
                using ( var unitOfWork = DependencyResolver.Resolve<IUnitOfWork>() ) {
                    await unitOfWork.PersonsRepository.Update( person );
                    await unitOfWork.SaveChangesAsync();
                    return true;
                }
            } catch {
                return false;
            }
        }
    }
}