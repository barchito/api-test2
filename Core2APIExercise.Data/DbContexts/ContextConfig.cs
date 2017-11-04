using Core2APIExercise.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Data.DbContexts
{
    public class  ContextConfig
    {
        public void ApplicationContextConfig(ModelBuilder modelBuilder, string schema = "")
        {

            modelBuilder.Entity<Person>().ToTable("Person", schema);
            modelBuilder.Entity<Identifier>().ToTable("Identifier", schema);
            modelBuilder.Entity<Identifier>().HasOne(x => x.Person).WithMany(x => x.Identifiers).HasForeignKey(x => x.PersonId);

        }

    }
}
