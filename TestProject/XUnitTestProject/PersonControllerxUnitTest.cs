using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TestProject.ApiResponse;
using TestProject.ApiResponseResult;
using TestProject.Controllers;
using TestProject.DatabaseContext;
using TestProject.Entites;
using TestProject.Enum;
using TestProject.ViewModel;
using Xunit;

namespace XUnitTestProject
{
    public class PersonControllerTest
    {
        private TestProjectDbContext _context;

        public PersonControllerTest()
        {
            InitContext();
        }

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TestProjectDbContext>().UseInMemoryDatabase();
            var context = new TestProjectDbContext(builder.Options);

            IEnumerable<Identifier> identifiers = Enumerable.Range(1, 10).Select(i =>
             new Identifier
             {
                 Id = Guid.NewGuid(),
                 Type = IdentifiersType.Email,
                 Value = $"value{i}"
             });

            var person = Enumerable.Range(1, 10).Select(i => new Person
            {
                Id = Guid.NewGuid(),
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                Identifiers = identifiers.ToList()
            });
            context.Person.AddRange(person);
            int changed = context.SaveChanges();

            _context = context;
        }

        [Fact(DisplayName = "Get all the persons with identfiers.")]
        public void GetAllPerson()
        {
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.Get();
            Assert.IsType<ApiResponse<List<PersonWithIdentfiersResult>>>(result.Value);
            var response = (ApiResponse<List<PersonWithIdentfiersResult>>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
            Assert.Equal(response.Data.Count.ToString(), _context.Person.Count().ToString());
        }

        [Fact(DisplayName = "Get all the person by firstname or lastname.")]
        public void GetAllPersonByFirstnameOrLastname()
        {
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.Get("FirstName1", "LastName1");
            Assert.IsType<ApiResponse<List<PersonWithIdentfiersResult>>>(result.Value);
            var response = (ApiResponse<List<PersonWithIdentfiersResult>>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
            Assert.Equal(response.Data.Count.ToString(), _context.Person.Where(x => x.FirstName == "FirstName1" || x.LastName == "LastName1").Count().ToString());
        }

        [Fact(DisplayName = "Get all the person who has specific identifiers.")]
        public void GetAllThePersonWhoHasSpecificIdentifiers()
        {
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.Get(IdentifiersType.Email);
            Assert.IsType<ApiResponse<List<PersonWithIdentfiersResult>>>(result.Value);
            var response = (ApiResponse<List<PersonWithIdentfiersResult>>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
            Assert.Equal(response.Data.Count.ToString(), _context.Person.Where(x => x.Identifiers.Any(a => a.Type == IdentifiersType.Email)).Count().ToString());
        }

        [Fact(DisplayName = "Create a person with idetifiers.")]
        public void Create()
        {
            PersonViewModel model = new PersonViewModel
            {
                FirstName = "firstname",
                LastName = "lastname",
                Identifiers = new List<IdentifiersViewModel> {
                    new IdentifiersViewModel{
                        Type = IdentifiersType.Email,
                        Value = "test@gmail.com"
                    }
                }
            };
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.Create(model);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }

        [Fact(DisplayName = "Create a person without identifiers.")]
        public void CreatePerson()
        {
            PersonWithoutIdentifiersViewModel model = new PersonWithoutIdentifiersViewModel
            {
                FirstName = "firstname",
                LastName = "lastname"
            };

            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.CreatePerson(model);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }

        [Fact(DisplayName = "Add new identifier to a person.")]
        public void CreateIdentifier()
        {
            var controller = new PersonsController(_context);
            CreateIdentifierViewModel model = new CreateIdentifierViewModel
            {
                PersonId = _context.Person.FirstOrDefault().Id,
                Type = IdentifiersType.Email,
                Value = "test@gmail.com"
            };

            var result = (ObjectResult)controller.CreateIdentifier(model);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }

        [Fact(DisplayName = "Delete identifier form a person.")]
        public void DeleteIdentifier()
        {
            var personId = _context.Person.FirstOrDefault().Id;
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.DeleteIdentifier(personId);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }

        [Fact(DisplayName = "Execute a logical delete on a person.")]
        public void Delete()
        {
            var personId = _context.Person.FirstOrDefault().Id;
            var controller = new PersonsController(_context);
            var result = (ObjectResult)controller.Delete(personId);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }


        [Fact(DisplayName = "Update a person without changing identifiers.")]
        public void Update()
        {
            var controller = new PersonsController(_context);
            PersonUpdateViewModel model = new PersonUpdateViewModel
            {
                PersonId = _context.Person.FirstOrDefault().Id,
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var result = (ObjectResult)controller.Update(model);
            Assert.IsType<ApiResponse<bool>>(result.Value);
            var response = (ApiResponse<bool>)result.Value;
            Assert.Equal(((int)HttpStatusCode.OK).ToString(), response.StatusCode.ToString());
        }

    }
}
