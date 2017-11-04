using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Data.DbContexts;
using Core2APIExercise.Data.Entities;
using Core2APIExercise.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core2APIExercise.Common.Repositories
{
    public class PersonRepository<T, TContext> : Repository<T, TContext>, IPersonRepository<T> where T : Person where TContext : DbContext
    {
        public PersonRepository(TContext context) : base(context)
        {
        }
        
        public void AddIdentifierToPerson(Guid Id, Identifier Identifier)
        {
            var person = base.Get(Id).Include("Identifiers").FirstOrDefault();
            if (person != null)
            {
                int newIde = (int)Identifier.IdentifierType;
                person.Identifiers.Add(Identifier);
                base.Update(person);
            }
        }
         

        public IQueryable<T> GetAllPersonByName(string Firstname, string LastName)
        {
            return base.GetAll().Where(p => p.LastName.Contains(LastName) && p.FirstName.Contains(Firstname));
        }

        /// <summary>
        /// get all the person who has specific identifiers
        /// </summary>
        /// <param name="Identifier"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllPersonByIdentifier(int Identifier)
        {
            //IdentifierType result;
            // if (Enum.TryParse(Identifier, out result)){}
           // var personByIdentifier = base.GetAll().Where(x => x.Identifiers.Any(y => y.IdentifierType ==  (IdentifierType)Enum.Parse(typeof(IdentifierType), Identifier)));
            var personByIdentifier = base.GetAll().Where(x => x.Identifiers.Any(y => (int)y.IdentifierType == Identifier));
            return personByIdentifier;
        }

        public void DeleteIdentifierFromPerson(Guid Id, int Identifier)
        {
            var person = base.Get(Id).Include(x => x.Identifiers).FirstOrDefault();
            if (person != null && person.Identifiers.Count() > 0)
            {
                var isFound = person.Identifiers.Where(x => (int)x.IdentifierType == Identifier).FirstOrDefault();
                if (isFound != null)
                {
                    person.Identifiers.Remove(isFound);
                    base.Update(person);
                }
            }
        }

        private void IsEntityNull(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
        }

     

        public bool IsIdentifierExist(int Identifier)
        {
            //var personByIdentifier = base.GetAll().Where(x => x.Identifiers.Any(y => y.IdentifierType == (IdentifierType)Enum.Parse(typeof(IdentifierType), Identifier)));
            var personByIdentifier = base.GetAll().Where(x => x.Identifiers.Any(y => (Int32) y.IdentifierType ==   Identifier ));
            return personByIdentifier.FirstOrDefault() != null;
         }
        public bool IsIdentifierExistToPerson(Guid personID, int Identifier)
        {
            var personByIdentifier = base.GetAll().Where(x => x.ID==personID && x.Identifiers.Any(y => (Int32)y.IdentifierType == Identifier));
            return personByIdentifier.FirstOrDefault() != null;
        }
    }
}
