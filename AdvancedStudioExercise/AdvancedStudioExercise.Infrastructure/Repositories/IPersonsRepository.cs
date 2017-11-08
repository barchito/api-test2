using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;

namespace AdvancedStudioExercise.Infrastructure.Repositories {
    public interface IPersonsRepository {
        Task<IEnumerable<PersonModel>> Get ();
        Task<PersonModel> Get ( Guid id );
        Task<IEnumerable<PersonModel>> GetByFirstNameOrLastName ( string firstNameOrLastName );
        Task<IEnumerable<PersonModel>> GetWithSpesificIdentifiers ( IEnumerable<Guid> identifiers );
        Task<Guid> Add ( PersonModel person );
        Task Delete ( Guid personId );
        Task Update ( PersonModel person );
    }
}