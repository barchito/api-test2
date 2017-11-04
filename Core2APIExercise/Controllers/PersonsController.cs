using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core2APIExercise.Common.Interfaces;
using Core2APIExercise.Data.Entities;
using AutoMapper;
using Core2APIExercise.Common;
using Core2APIExercise.Data.DbContexts; 
using Core2APIExercise.Data.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core2APIExercise.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        private IPersonRepository<Person> _personRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Mapping person controller & unitwork objec initilization
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public PersonsController(UnitOfWork<ApiContext> unitOfWork, IMapper mapper)
        {
            _personRepository = unitOfWork.PersonRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///  get all the persons with identfiers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PersonIndentifierModel> GetAll()
        {
            var query = _personRepository.GetAll().Include("Identifiers")
             .Select(d => _mapper.Map<PersonIndentifierModel>(d));
            return query.AsEnumerable();
            //var query = _personRepository.GetAll(x => x.Include(p => p.Identifiers));
            //query.Load();
            //var result= query.Select(d => _mapper.Map<PersonIndentifierModel>(d));
            //return result;
        }

        /// <summary>
        ///   get all the person by firstname or lastname
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FirstName/{FirstName?}/{LastName?}", Name ="GetPersonsByName")]
        public IEnumerable<PersonModel> GetPersonByName(string FirstName, string LastName="")
        {
            var query = _personRepository.GetAllPersonByName(FirstName, LastName)
                .Select(d => _mapper.Map<PersonModel>(d));
            return query.ToList();
        }

      
        /// <summary>
        /// Get Person by GUID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name = "GetPerson")]
        public IActionResult GetById(Guid id)
        {
            var dto = _personRepository.Get(id)
                .Select(d => _mapper.Map<PersonModel>(d))
                .FirstOrDefault();
            if (dto == null)
            {
                return NotFound();
            }

            return new ObjectResult(dto);
        }

        /// <summary>
        ///  get all the person who has specific identifiers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Identifier/{Identifier:int}", Name = "GetPersonByIdentifier")]
        public IEnumerable<PersonModel> GetPersonByIdentifier(int Identifier)
        {
            var query = _personRepository.GetAllPersonByIdentifier(Identifier)
                .Select(d => _mapper.Map<PersonModel>(d));
            return query.ToList();
        }


        /// <summary>
        /// create a person with idetifiers. ( if we did not pass identifier it would not create)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Person
        ///     {
        ///         
        ///     }
        ///
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>A newly-created TodoItem</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>        
        [HttpPost]
        [ProducesResponseType(typeof(PersonModel), 201)]
        [ProducesResponseType(typeof(PersonModel), 400)]
        [SwaggerResponse(422, type: null, description: "Model Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public async Task<IActionResult> Create([FromBody] PersonIndentifierModel dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            var person = _mapper.Map<Person>(dto);
            await _personRepository.AddAsync(person);
            await _personRepository.SaveChangesAsync();
            return CreatedAtRoute("GetPerson", new { id = person.ID }, _mapper.Map<PersonIndentifierModel>(person));
        }

        /// <summary>
        ///  Add new identifier to a person
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     identifier /Person/1/CreateIdentifier
        ///     {
        ///         
        ///     }
        ///
        /// </remarks>
        /// <param name="dto"></param>
        /// <returns>A newly-created TodoItem</returns>
        /// <response code="201">Returns the newly-created item</response>
        /// <response code="400">If the item is null</response>        
        [HttpPost("{id:guid}/Identifier/", Name = "AddIdentifier")]
        [ProducesResponseType(typeof(IdentifierModel), 201)]
        [ProducesResponseType(typeof(IdentifierModel), 400)]

        [SwaggerResponse(422, type: null, description: "Model Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public async Task<IActionResult> AddIdentifier(Guid id, [FromBody] IdentifierModel dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var Identifier = _mapper.Map<Identifier>(dto);
            var person = _personRepository.Get(id).FirstOrDefault();
            if (person != null)
            {
                //if (_personRepository.IsIdentifierExist(Identifier.IdentifierType.GetEnumDisplayName()))
                if (_personRepository.IsIdentifierExistToPerson(id, Convert.ToInt32(Identifier.IdentifierType)))
                {
                    return NotFound(new ApiResponse(404, $"IDENTIFIER is already added to person"));
                }

                _personRepository.AddIdentifierToPerson(id, Identifier);
                await _personRepository.SaveChangesAsync();
                return CreatedAtRoute("GetPerson", new { ID = person.ID }, _mapper.Map<PersonModel>(person));
            }
            return BadRequest();

        }

        /// <summary>
        ///  delete identifier form a person
        /// </summary>
        /// <remarks>
        [HttpDelete("{id:guid}/Identifier/{Identifier:int}", Name = "DeleteIdentifier")]
        public async Task<IActionResult> DeleteIdentifier(Guid id, int Identifier)
        {
            if (ModelState.IsValid)
            {
                var person = _personRepository.Get(id).FirstOrDefault();
                if (person == null)
                {
                    return NotFound();
                }

                if (_personRepository.IsIdentifierExistToPerson(id, Identifier))
                {
                    _personRepository.DeleteIdentifierFromPerson(id, Identifier);
                    await _personRepository.SaveChangesAsync();
                }
                else
                {
                    return NotFound(new ApiResponse(404, $"IDENTIFIER not found with person"));
                }
            }
            else
            {
                return BadRequest();
            }
            //return new NoContentResult();
            return new OkResult();
        }

        /// <summary>
        ///  update a person without changing identifiers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PersonModel), 201)]
        [ProducesResponseType(typeof(PersonModel), 400)]
        [SwaggerResponse(422, type: null, description: "Model Validation Error")]
        [SwaggerResponse(400, type: null, description: "Bad Request")]
        [SwaggerResponse(500, type: null, description: "Internal Server Error")]
        public async Task<IActionResult> Update(Guid id, [FromBody]PersonModel dto)
        {
            if (dto == null || id != dto.ID)
            {
                return BadRequest();
            }

            var newperson = _mapper.Map<Person>(dto);

            var oldPerson = _personRepository.Get(newperson.ID).FirstOrDefault();
            if (oldPerson == null)
            {
                return NotFound();
            }

            try
            {
                oldPerson.FirstName = newperson.FirstName;
                oldPerson.LastName = newperson.LastName;
                _personRepository.Update(oldPerson);
                await _personRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                //todo: log excecption
                return BadRequest();
            }
           
            return new OkResult();
        }


        /// <summary>
        ///  delete a person for test
        /// </summary>
        /// <remarks>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var person = _personRepository.Get(id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }

            _personRepository.Delete(person);
            await _personRepository.SaveChangesAsync();

            //return new NoContentResult();
            return new OkResult();

        }
    }
}