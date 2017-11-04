using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Common.Repositories
{
    public class IdentifierRepository<T, TContext> : Repository<T, TContext>, IIdentifierRepository<T> where T : Identifier where TContext : DbContext
    {
        public IdentifierRepository(TContext context) : base(context)
        {

        }
    }
} 