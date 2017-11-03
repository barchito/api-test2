using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTest.DBContext;
using ApiTest.DBContext.Entities;
using Microsoft.EntityFrameworkCore;
using ApiTest.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace ApiTest.Controllers
{
    /// <summary>
    /// Controller to perform operations related to person
    /// </summary>
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller, IDisposable
    {
        private PersonsContext _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db"></param>
        public PersonController(PersonsContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Get all the persons with identfiers.
        /// </summary>
        /// <returns></returns>
        [Route("all"), HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns the collection of persons", type: typeof(List<PersonModel>))]
        public IActionResult GetPersons()
        {
            var persons = _db.Persons.Include(x => x.Identifiers).ToList();

            return Ok(ProcessPersonModelList(persons));
        }

        /// <summary>
        /// Get all the person by firstname or lastname
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns the persons matched with FirstName and LastName", type: typeof(List<PersonModel>))]
        public IActionResult GetPersonByName([FromBody]PersonModel person)
        {
            //var persons = _db.Persons.Include(x => x.Identifiers).Where(x => (x.FirstName.Equals(!string.IsNullOrWhiteSpace(person.FirstName) ? person.FirstName : x.FirstName))
            //                                    && (x.LastName.Equals(!string.IsNullOrWhiteSpace(person.LastName) ? person.LastName : x.LastName))).ToList();
            List<Person> persons = new List<Person>();
            if (string.IsNullOrEmpty(person.FirstName) && string.IsNullOrEmpty(person.LastName))
            {
                persons = _db.Persons.ToList();
            }
            else
            {
                persons = _db.Persons.Include(x => x.Identifiers).Where(x => (x.FirstName.Equals(person.FirstName))
                                                   || (x.LastName.Equals(person.LastName))).ToList();
            }

            return Ok(ProcessPersonModelList(persons));
        }

        /// <summary>
        /// Get all the person who has specific identifiers
        /// </summary>
        /// <param name="identifiers">List of identifiers to compare</param>
        /// <returns></returns>
        [Route("personByIdentifiers")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns the persons matched with FirstName and LastName", type: typeof(List<PersonModel>))]
        public IActionResult GetPersonByIdentifiers([FromBody]List<Guid> identifiers)
        {
            List<PersonModel> personsList = new List<PersonModel>();

            var identifier = _db.Identifiers.Include(x => x.Person).Where(x => identifiers.Contains(x.Id)).ToList();
            identifier.ForEach(x =>
            {
                var personModel = MapPersonWithPersonModel(x.Person);
                if (!personsList.Any(p => p.Id.Equals(personModel.Id)))
                {
                    personsList.Add(personModel);
                }
            });

            return Ok(personsList);
        }
        /// <summary>
        /// Create a person with or without idetifiers
        /// </summary>
        /// <param name="person">Person info to create</param>
        /// <returns></returns>
        [Route("createPerson")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns new created person", type: typeof(PersonModel))]
        public IActionResult CreatePerson([FromBody]PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person personToAdd = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            if (person.Identifiers != null)
            {
                personToAdd.Identifiers = new List<Identifier>();
                person.Identifiers.ForEach(x =>
                {
                    personToAdd.Identifiers.Add(new Identifier
                    {
                        Type = x.Type,
                        Value = x.Value
                    });
                });
            }

            _db.Persons.Add(personToAdd);
            _db.SaveChanges();

            return Ok(person);
        }

        /// <summary>
        /// Add new identifier to a person
        /// </summary>
        /// <param name="identifier">Identifier info</param>
        /// <returns></returns>
        [Route("createIdentifier")]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns new created identifier", type: typeof(PersonModel))]
        public IActionResult CreateIdentifier([FromBody]IdentifierModel identifier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _db.Persons.Include(x => x.Identifiers).FirstOrDefault(x => x.Id.Equals(identifier.PersonId));

            if (person == null)
            {
                return BadRequest("Person not found");
            }

            var identifierToAdd = new Identifier
            {
                Value = identifier.Value,
                Type = identifier.Type
            };


            person.Identifiers.Add(identifierToAdd);
            _db.SaveChanges();

            return Ok(identifierToAdd);
        }

        /// <summary>
        /// Delete identifier form a person
        /// </summary>
        /// <param name="id">identifier id to delete</param>
        /// <returns></returns>
        [Route("deleteIdentifier/{id}")]
        [HttpDelete]
        public IActionResult DeleteIdentifier(Guid id)
        {
            var identifier = _db.Identifiers.FirstOrDefault(x => x.Id.Equals(id));

            if (identifier == null)
            {
                return BadRequest("Identifier not found");
            }

            _db.Identifiers.Remove(identifier);
            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Execute a logical delete on a person
        /// </summary>
        /// <param name="personId">Unique Person id to delete</param>
        /// <returns></returns>
        [Route("deletePerson/{personId}")]
        [HttpDelete]
        public IActionResult DeletePerson(Guid personId)
        {
            var person = _db.Persons.Include(x => x.Identifiers).FirstOrDefault(x => x.Id.Equals(personId));

            if (person == null)
            {
                return BadRequest("Person not found");
            }

            if (person.Identifiers.Count > 0)
            {
                return BadRequest("Can't delete person because it contains identifiers.");
            }

            _db.Persons.Remove(person);
            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Update a person without changing identifiers
        /// </summary>
        /// <param name="person">Person info to update</param>
        /// <returns></returns>
        [Route("UpdatePerson")]
        [HttpPut]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Returns udpated person", type: typeof(PersonModel))]
        public IActionResult UpdatePerson([FromBody]PersonModel person)
        {
            var personToUpdate = _db.Persons.Include(x => x.Identifiers).FirstOrDefault(x => x.Id.Equals(person.Id));

            if (personToUpdate == null)
            {
                return BadRequest("Person not found");
            }

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;

            _db.SaveChanges();

            return Ok(MapPersonWithPersonModel(personToUpdate));
        }

        private List<PersonModel> ProcessPersonModelList(List<Person> persons)
        {
            List<PersonModel> personsList = new List<PersonModel>();

            persons.ForEach(x =>
            {
                var person = new PersonModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                };
                if (x.Identifiers != null)
                {
                    person.Identifiers = new List<IdentifierModel>();
                    x.Identifiers.ForEach(ident =>
                    {
                        person.Identifiers.Add(new IdentifierModel
                        {
                            Id = ident.Id,
                            PersonId = ident.PersonId,
                            Type = ident.Type,
                            Value = ident.Value
                        });
                    });
                }

                personsList.Add(person);
            });

            return personsList;
        }

        private PersonModel MapPersonWithPersonModel(Person person)
        {
            var personModel = new PersonModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
            if (person.Identifiers != null)
            {
                personModel.Identifiers = new List<IdentifierModel>();
                person.Identifiers.ForEach(ident =>
                {
                    personModel.Identifiers.Add(new IdentifierModel
                    {
                        Id = ident.Id,
                        PersonId = ident.PersonId,
                        Type = ident.Type,
                        Value = ident.Value
                    });
                });
            }

            return personModel;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            _db.Dispose();
        }
    }
}