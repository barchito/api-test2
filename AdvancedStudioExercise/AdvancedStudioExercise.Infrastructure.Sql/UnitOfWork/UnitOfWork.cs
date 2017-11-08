using System;
using System.Threading.Tasks;
using AdvancedStudioExercise.Infrastructure.Repositories;
using AdvancedStudioExercise.Infrastructure.Sql.Repositories;
using AdvancedStudioExercise.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Options;

namespace AdvancedStudioExercise.Infrastructure.Sql.UnitOfWork {
    public class UnitOfWork : IUnitOfWork {
        private readonly IOptions<AppConnectionStrings> _options;
        private AdvancedStudioExerciseContext _context;
        private bool _disposed;
        private IIdentifiersRepository _identifiersRepository;
        private IPersonsRepository _personsRepository;

        public UnitOfWork ( IOptions<AppConnectionStrings> options ) {
            _options = options;
            AutoMapperConfig.Configure();
        }

        private AdvancedStudioExerciseContext CurrentContext => _context ?? (_context = new AdvancedStudioExerciseContext( _options ));

        public IPersonsRepository PersonsRepository {
            get { return _personsRepository = _personsRepository ?? new PersonsRepository( CurrentContext ); }
        }

        public IIdentifiersRepository IdentifiersRepository {
            get { return _identifiersRepository = _identifiersRepository ?? new IdentifiersRepository( CurrentContext ); }
        }

        public void Dispose () {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        public void SaveChanges () {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync () {
             await _context.SaveChangesAsync();
        }

        protected virtual void Dispose ( bool disposing ) {
            if ( !_disposed ) {
                if ( disposing ) {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}