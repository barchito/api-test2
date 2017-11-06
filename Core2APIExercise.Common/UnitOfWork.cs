using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Common.Repositories;
using Core2APIExercise.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core2APIExercise.Common
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private TContext _context;
        private IPersonRepository<Person> _personRepository;
        private IIdentifierRepository<Identifier> _identifierRepository;

        public UnitOfWork()
        {

        }
        public UnitOfWork(TContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        //public virtual IRepository<Person> PersonRepository
        //{
        //    get => _personRepository = _personRepository ?? new Repository<Person, TContext>(_context);
        //}
        public virtual IPersonRepository<Person> PersonRepository
        {
            get => _personRepository = _personRepository ?? new  PersonRepository<Person, TContext>(_context);
        }

        public virtual IIdentifierRepository<Identifier> IdentifierRepository
        {
            get => _identifierRepository = _identifierRepository ?? new  IdentifierRepository<Identifier, TContext>(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
