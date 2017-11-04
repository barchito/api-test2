using Core2APIExercise.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core2APIExercise.Common.Interfaces
{
    public interface IPersonRepository<T> : IRepository<T> where T : Person
    {

        //get all the person by firstname or lastname
        IQueryable<T> GetAllPersonByName(string Firstname,string LastName);

        //get all the person who has specific identifiers
        IQueryable<T> GetAllPersonByIdentifier(int IdentifierType);

        //add new identifier to a person,
        void AddIdentifierToPerson(Guid Id, Identifier Identifier);

        //delete identifier form a person
        void DeleteIdentifierFromPerson(Guid Id, int Identifier);

        //check if exists specific identifier
        bool IsIdentifierExist(int Identifier);

        //check if exists the person with specific identifier
        bool IsIdentifierExistToPerson(Guid personID, int Identifier);
    }
}
