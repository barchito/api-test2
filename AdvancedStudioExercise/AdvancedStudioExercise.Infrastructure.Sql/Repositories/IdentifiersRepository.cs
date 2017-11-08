using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Domain.Models;
using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.Sql.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedStudioExercise.Infrastructure.Sql.Repositories {
    public class IdentifiersRepository : IIdentifiersRepository {
        private readonly AdvancedStudioExerciseContext _dbContext;

        public IdentifiersRepository ( AdvancedStudioExerciseContext dbContext ) {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add ( IdentifierModel identifier ) {
            var entity = Mapper.Map<Identifier>( identifier );
            await _dbContext.Identifiers.AddAsync( entity );
            await _dbContext.SaveChangesAsync( );
            return entity.Id;
        }

        public async Task Delete ( Guid identifierId ) {
            var entity = await _dbContext.Identifiers.AsNoTracking().SingleOrDefaultAsync( i => i.Id == identifierId );
            _dbContext.Remove( entity );
        }
    }
}