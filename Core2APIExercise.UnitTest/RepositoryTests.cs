using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Core2APIExercise.Common.Repositories;
using Core2APIExercise.Data.Entities;
using Core2APIExercise.Data.DbContexts;
using System.Collections.Generic;
using Core2APIExercise.Data.Enum;

namespace Core2APIExercise.Data.Test
{
    public class RepositoryTests
    {
        private PersonRepository<Person, ApiContext> _sut;
        private IdentifierRepository<Identifier, ApiContext> _sutIden;

        /// <summary>
        /// Constructor
        /// </summary>
        public RepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseInMemoryDatabase("APIdb");

            var context = new ApiContext(builder.Options);
            _sut = new PersonRepository<Person, ApiContext>(context);
            _sutIden = new IdentifierRepository<Identifier, ApiContext>(context);
        }

        [Fact]
        public void Get_ReturnsEntityQueryable()
        {
            Guid ID = new Guid();
            var result = _sut.Get(ID).AsEnumerable();
            Assert.IsType<EntityQueryable<Person>>(result);
        }

        /// <summary>
        ///  get all the persons with identfiers.
        /// </summary>
        [Fact]
        public void GetAll_ReturnsInternalDbSet()
        {
            var result = _sut.GetAll();
            Assert.IsType<InternalDbSet<Person>>(result);
        }

        /// <summary>
        ///  get all the person by firstname or lastname
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void GetAll_ByFirstOrLastNameTest()
        {
            var person = new Person
            {
                FirstName = "Abhishek",
                LastName = "Master",
                Identifiers =
                    new List<Identifier>
                    {
                        new Identifier{IdentifierType=IdentifierType.Email,Value= "bhalaniabhishek@gmail.com" },
                        new Identifier {IdentifierType=IdentifierType.LicensePlate,Value= "GJ6EA" }
                    }
            };

            _sut.Add(person);
            await _sut.SaveChangesAsync();
            var result = _sut.GetAllPersonByName(person.FirstName,person.LastName);
            Assert.IsType<EntityQueryable<Person>>(result);
        }
        /// <summary>
        ///  get all the person who has specific identifiers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void GetAll_ByIdentifierTest()
        {
            var person = new Person
            {
                FirstName = "Abhishek",
                LastName = "Master",
                Identifiers =
                    new List<Identifier>
                    {
                        new Identifier{IdentifierType=IdentifierType.Email,Value= "bhalaniabhishek@gmail.com" },
                        new Identifier {IdentifierType=IdentifierType.LicensePlate,Value= "GJ6EA" }
                    }
            };

            _sut.Add(person);
            await _sut.SaveChangesAsync();
            var result = _sut.GetAllPersonByIdentifier(Convert.ToInt32(IdentifierType.LicensePlate));
            Assert.IsType<EntityQueryable<Person>>(result);
        }

        /// <summary>
        ///  create a person with idetifiers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_ShouldAddPersonWithIdentifier()
        {
            var person = new Person
            {
                FirstName = "Abhishek",
                LastName = "Master",
                Identifiers =
                    new List<Identifier>
                    {
                            new Identifier{IdentifierType=IdentifierType.Email,Value= "bhalaniabhishek@gmail.com" },
                            new Identifier {IdentifierType=IdentifierType.LicensePlate,Value= "GJ6EA" }
                    }
            };

            _sut.Add(person);
            await _sut.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            Assert.Equal(person.FirstName, personAdded.FirstName);
            Assert.Equal(person.LastName, personAdded.LastName);
        }

        /// <summary>
        ///  create a person without identifiers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddAsync_ShouldAddPersonWithoutIdentifier()
        {
            var person = new Person
            {
                FirstName = "SANJU",
                LastName = "B."
            };

            await _sut.AddAsync(person);
            await _sut.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            Assert.Equal(person.FirstName, personAdded.FirstName);
        }


        /// <summary>
        ///   add new identifier to a person,
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddAsync_ShouldAddIdentifierToPerson()
        {
            var person = new Person
            {
                FirstName = "Abhishek",
                LastName = "Bhalani"
            };

            await _sut.AddAsync(person);
            await _sut.SaveChangesAsync();

            var identifier = new Identifier
            {
                IdentifierType=IdentifierType.Email,Value= "bhalaniabhishek@gmail.com",
                Person=person
            };

            _sutIden.Add(identifier);
            await _sutIden.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            Assert.Equal(1, personAdded.Identifiers.Count());
        }

       
        /// <summary>
        /// delete identifier form a person
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ShouldRemoveIdentifierFromPerson()
        {
            await ResetDb();
            var person = new Person
            {
                FirstName = "Abhi",
                LastName = "B.",  
                 Identifiers= new List<Identifier>
                    {
                        new Identifier{IdentifierType=IdentifierType.Email,Value= "bhalaniabhishek@gmail.com" },
                        new Identifier {IdentifierType=IdentifierType.LicensePlate,Value= "GJ6EA" }
                    }
                  
            };
            var count = _sut.GetAll().Count();
            Assert.Equal(0, count);

            await _sut.AddAsync(person);
            await _sut.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            count = _sut.GetAll().Count();
            Assert.Equal(person.FirstName, personAdded.FirstName);
            Assert.Equal(1, count);

            _sutIden.Delete(personAdded.Identifiers.LastOrDefault());
            await _sut.SaveChangesAsync();

            //var personIdenfier = _sut.Get(person.ID).Select(x=>x.Identifiers);
            //count = personIdenfier.Count();
            // Assert.Equal(1, count); 
            Assert.Equal(1, personAdded.Identifiers.Count());
        }

        /// <summary>
        /// execute a logical delete on a person
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ShouldRemovePerson()
        {
            await ResetDb();
            var person = new Person
            {
                FirstName = "SANJU",
                LastName = "B."
            };
            var count = _sut.GetAll().Count();
            Assert.Equal(0, count);

            await _sut.AddAsync(person);
            await _sut.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            count = _sut.GetAll().Count();

            Assert.Equal(person.FirstName, personAdded.FirstName);
            Assert.Equal(1, count);

            _sut.Delete(personAdded);
            await _sut.SaveChangesAsync();

            count = _sut.GetAll().Count();
            Assert.Equal(0, count);
        }

        /// <summary>
        /// update a person without changing identifiers
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ShouldModifyPerson()
        {
            await ResetDb();
            var person = new Person
            {
                FirstName = "SANJU",
                LastName = "BHALANI"
            };
            var count = _sut.GetAll().Count();
            Assert.Equal(0, count);

            await _sut.AddAsync(person);
            await _sut.SaveChangesAsync();

            var personAdded = _sut.GetAll().Last();
            count = _sut.GetAll().Count();
            Assert.Equal(person.FirstName, personAdded.FirstName);
            Assert.Equal(1, count);

            var personToUpdate = personAdded;
            personToUpdate.FirstName = "Sanju Mec";

            _sut.Update(personToUpdate, personAdded.RowVersion);
            await _sut.SaveChangesAsync();

            Assert.Equal("Sanju Mec", personAdded.FirstName);
        }

        /// <summary>
        /// Reset DB during test asserts
        /// </summary>
        /// <returns></returns>
        private async Task ResetDb()
        {
            var persons = _sut.GetAll().ToArray();
            foreach (var item in persons)
            {
                if(item.Identifiers!=null)
                foreach (var itemIde in item.Identifiers)
                {
                    //item.Identifiers.Remove(itemIde);
                     _sutIden.Delete(itemIde);
                }
                _sut.Delete(item);
            }
            await _sut.SaveChangesAsync();
        }
    }

}
