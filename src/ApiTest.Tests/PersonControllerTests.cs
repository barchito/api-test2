using ApiTest.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;
using ApiTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using ApiTest.DBContext.Entities;
using System.Collections.Generic;
using System.Net;
using ApiTest.Models;
using System.Threading.Tasks;

namespace ApiTest.Tests
{
    public class PersonControllerTests
    {
        private PersonsContext _db;
        private PersonController _controller;
        public PersonControllerTests()
        {
            InitContext();
            _controller = new PersonController(_db);
        }

        private void InitContext()
        {
            if (_db == null)
            {
                var builder = new DbContextOptionsBuilder<PersonsContext>().UseInMemoryDatabase();
                var context = new PersonsContext(builder.Options);
                var persons = Enumerable.Range(1, 10)
                    .Select(i => new Person
                    {
                        FirstName = $"Person{i}",
                        LastName = $"Last{i}",
                        Identifiers = new List<Identifier>
                        {
                        new Identifier {Type=IdentifierTypes.Email,Value=$"Identifier{i}" },
                        new Identifier {Type=IdentifierTypes.Email,Value=$"Identifier{i+1}" }
                        }
                    });

                context.Persons.AddRange(persons);

                int changed = context.SaveChanges();

                _db = context;
            }
        }

        [Fact]
        public async Task TestGetPersons()
        {
            var result = await _controller.GetPersons();
            ProcessResult(result);
        }

        [Fact]
        public void TestGetPersonByName()
        {
            PersonModel person = new PersonModel { FirstName = "Person1", LastName = "Last1" };
            var result = _controller.GetPersonByName(person);
            ProcessResult(result);
        }

        [Fact]
        public void TestGetPersonByIdentifiers()
        {
            var identifiers = _db.Identifiers.Take(2).ToList();
            List<Guid> identifierIds = new List<Guid> { identifiers[0].Id, identifiers[1].Id };
            var result = _controller.GetPersonByIdentifiers(identifierIds);
            ProcessResult(result);
        }

        [Fact]
        public void TestCreatePerson()
        {
            //testing with identifiers
            PersonModel person = new PersonModel
            {
                FirstName = $"Person1",
                LastName = $"Last1",
                Identifiers = new List<IdentifierModel>
                        {
                        new IdentifierModel {Type=IdentifierTypes.Email,Value=$"Identifier 1" },
                        new IdentifierModel {Type=IdentifierTypes.Email,Value=$"Identifier 2" }
                        }
            };

            var result = _controller.CreatePerson(person) as OkObjectResult;
            Assert.True(result.StatusCode == (int)HttpStatusCode.OK);

            //testing without identifiers
            person = new PersonModel
            {
                FirstName = $"Person2",
                LastName = $"Last2"
            };

            var result1 = _controller.CreatePerson(person);
            ProcessResult(result);
        }

        [Fact]
        public void TestCreateIdentifier()
        {
            IdentifierModel identifier = new IdentifierModel
            {
                Type = IdentifierTypes.Email,
                Value = "Test Value",
                PersonId = _db.Persons.FirstOrDefault().Id
            };

            var result = _controller.CreateIdentifier(identifier);
            ProcessResult(result);
        }

        [Fact]
        public void TestDeleteIdentifier()
        {
            var identifierId = _db.Identifiers.FirstOrDefault().Id;
            var result = _controller.DeleteIdentifier(identifierId);
            ProcessResult(result);
        }

        [Fact]
        public void TestDeletePerson()
        {
            var personId = _db.Persons.FirstOrDefault().Id;
            var result = _controller.DeletePerson(personId);
            ProcessResult(result);
        }

        [Fact]
        public void TestUpdatePerson()
        {
            PersonModel person = new PersonModel
            {
                FirstName = "Updated Person",
                LastName = "Updated Last",
                Id = _db.Persons.FirstOrDefault().Id
            };
            var result = _controller.UpdatePerson(person);

            ProcessResult(result);
        }

        private void ProcessResult(object result)
        {
            if (result is BadRequestObjectResult)
            {
                Assert.True(((BadRequestObjectResult)result).StatusCode == (int)HttpStatusCode.BadRequest);
            }
            else if (result is OkResult)
            {
                Assert.True(((OkResult)result).StatusCode == (int)HttpStatusCode.OK);
            }
            else if (result is OkObjectResult)
            {
                Assert.True(((OkObjectResult)result).StatusCode == (int)HttpStatusCode.OK);
            }
        }
    }
}
