using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestProject.Entites;
using TestProject.Enum;
using TestProject.Exceptions;
using TestProject.ViewModel;

namespace TestProject.DatabaseContext
{
    /// <summary>
    /// 
    /// </summary>
    public class GenericDataRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IList<Person> GetPersonWithIdentfier(TestProjectDbContext context)
        {
            List<Person> list = context.Person.Include(x => x.Identifiers).Where(x => !x.Deleted).ToList();
            return list;
        }

        /// <summary>
        /// Get all Person By FirstName Or LastName
        /// </summary>
        /// <param name="context"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public IList<Person> GetPersonByFirstNameOrLastName(TestProjectDbContext context, string firstname, string lastname)
        {
            List<Person> list = context.Person.Where(x => x.FirstName.Equals(firstname) || x.LastName.Equals(lastname) && !x.Deleted).Include(x => x.Identifiers).ToList();
            return list;
        }

        /// <summary>
        /// GetPersonBySpecificIdentifiers
        /// </summary>
        /// <param name="context"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public IList<Person> GetPersonBySpecificIdentifiers(TestProjectDbContext context, IdentifiersType enumType)
        {
            return context.Person.Where(x => x.Identifiers.Any(i => i.Type == enumType) && !x.Deleted).ToList();
        }

        /// <summary>
        /// Create person
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        public void CreatePerson(TestProjectDbContext context, Person model)
        {
            context.Person.Add(model);
            context.Entry(model).State = EntityState.Added;
            context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public Person GetPersonbyId(TestProjectDbContext context, Guid personId)
                => context.Person.FirstOrDefault(x => x.Id == personId && !x.Deleted);

        /// <summary>
        /// Create Identifier
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        public void CreateIdentifier(TestProjectDbContext context, Identifier model)
        {

            context.Identifier.Add(model);
            context.Entry(model).State = EntityState.Added;
            context.SaveChanges();

        }

        /// <summary>
        /// Delete Identifier
        /// </summary>
        /// <param name="context"></param>
        /// <param name="personId"></param>
        public void DeleteIdentifier(TestProjectDbContext context, Guid personId)
        {
            Person person = context.Person.AsNoTracking().FirstOrDefault(x => x.Id == personId);

            if (person == null)
            {
                throw new ErrorException("Person Id is invalid");
            }
            var identifiers = context.Identifier.Where(x => x.PersonId == personId);
            foreach (var identifier in identifiers)
            {
                context.Entry(identifier).State = EntityState.Deleted;
            }
            context.Identifier.RemoveRange();
            context.SaveChanges();

        }

        /// <summary>
        /// Delete Person
        /// </summary>
        /// <param name="context"></param>
        /// <param name="person"></param>
        public void DeletePerson(TestProjectDbContext context, Person person)
        {
            person.Deleted = true;
            context.Entry(person).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// update person
        /// </summary>
        /// <param name="context"></param>
        /// <param name="person"></param>
        public void UpdatePerson(TestProjectDbContext context, Person person)
        {
            context.Entry(person).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
