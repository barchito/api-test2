using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestProject.Entites;

namespace TestProject.DatabaseContext
{
    /// <summary>
    /// Test Project Database Context
    /// </summary>
    public class TestProjectDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options)
            : base(options)
        { }
        
        /// <summary>
        /// Person Entity
        /// </summary>
        public DbSet<Person> Person { get; set; }

        /// <summary>
        /// Identifier Entity
        /// </summary>
        public DbSet<Identifier> Identifier { get; set; }

        /// <summary>
        /// OnModelCreating override method of DbContext class.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
