using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;

namespace AdvancedStudioExercise.Domain.Services {
    public interface IPersonService {
        Task<IEnumerable<PersonModel>> GetAll ();
        Task<IEnumerable<PersonModel>> GetByFirstNameOrLastName ( string firstNameOrLastName );
        Task<IEnumerable<PersonModel>> GetWithSpesificIdentifiers ( IEnumerable<Guid> identifiers );
        Task<Guid> Create ( PersonModel person );
        Task<bool> DeletePerson ( Guid personId );
        Task<bool> UpdatePerson ( PersonModel person );
    }
}