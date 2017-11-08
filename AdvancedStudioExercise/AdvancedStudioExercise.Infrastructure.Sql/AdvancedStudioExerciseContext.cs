using System;
using System.IO;
using AdvancedStudioExercise.Infrastructure.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AdvancedStudioExercise.Infrastructure.Sql {
    public class AdvancedStudioExerciseContext : DbContext {
        private readonly IOptions<AppConnectionStrings> _options;
        
        public AdvancedStudioExerciseContext ( IOptions<AppConnectionStrings> options ) {
            _options = options;
        }

        public AdvancedStudioExerciseContext ( DbContextOptions<AdvancedStudioExerciseContext> options ) : base( options ) {
        }

        public DbSet<IdentifierType> IdentifierTypes { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder ) {
            if ( !optionsBuilder.IsConfigured ) {
                optionsBuilder.UseSqlServer( _options.Value.DefaultConnection );
            }
        }

        protected override void OnModelCreating ( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<IdentifierType>()
                .Property(it => it.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<IdentifierType>()
                .Property(it => it.Label)
                .IsRequired();

            modelBuilder.Entity<Identifier>()
                .Property(it => it.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Identifier>()
                .Property(i => i.Value)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .Property(it => it.Id)
                .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Person>()
                .Property(p => p.FirstName)
                .IsRequired();
            modelBuilder.Entity<Person>()
                .Property(p => p.LastName)
                .IsRequired();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdvancedStudioExerciseContext> {
        public AdvancedStudioExerciseContext CreateDbContext ( string[] args ) {
            var builder = new DbContextOptionsBuilder<AdvancedStudioExerciseContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new AdvancedStudioExerciseContext( builder.Options );
        }
    }
}