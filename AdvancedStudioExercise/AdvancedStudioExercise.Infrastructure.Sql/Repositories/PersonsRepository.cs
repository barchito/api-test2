using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedStudioExercise.Infrastructure.Sql.Repositories {
    public class PersonsRepository : IPersonsRepository {
        private readonly AdvancedStudioExerciseContext _dbContext;

        public PersonsRepository ( AdvancedStudioExerciseContext dbContext ) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PersonModel>> Get () {
            var entities = await _dbContext.Persons.Include( p => p.Identifiers ).ToListAsync();
            return Mapper.Map<IEnumerable<PersonModel>>( entities );
        }

        public async Task<PersonModel> Get ( Guid id ) {
            var entity = await _dbContext.Persons.FindAsync( id );
            return entity != null ? Mapper.Map<PersonModel>( entity ) : null;
        }

        public Task<IEnumerable<PersonModel>> GetByFirstNameOrLastName ( string firstNameOrLastName ) {
            var normalizedValue = firstNameOrLastName.ToUpper();
            var entities = _dbContext.Persons.Where( p => p.FirstName.ToUpper() == normalizedValue || p.LastName.ToUpper() == normalizedValue );
            return Task.FromResult(Mapper.Map<IEnumerable<PersonModel>>( entities ));
        }

        public Task<IEnumerable<PersonModel>> GetWithSpesificIdentifiers ( IEnumerable<Guid> identifiers ) {
            var entities = _dbContext.Identifiers.Include( i => i.Person ).ThenInclude( p => p.Identifiers )
                .Where( i => identifiers.Any( id => id == i.Id ) );
            var persons = entities.Select( i => i.Person );
            return Task.FromResult( Mapper.Map<IEnumerable<PersonModel>>( persons ) );
        }

        public async Task<Guid> Add ( PersonModel person ) {
            var entity = Mapper.Map<Person>( person );
            await _dbContext.AddAsync( entity );
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete ( Guid personId ) {
            var entity = await _dbContext.Persons.AsNoTracking().SingleOrDefaultAsync(i => i.Id == personId);
            _dbContext.Remove(entity);
        }

        public async Task Update ( PersonModel person ) {
            var entity = await _dbContext.Persons.FindAsync( person.Id );
            entity.FirstName = person.FirstName;
            entity.LastName = person.LastName;
        }
    }
}